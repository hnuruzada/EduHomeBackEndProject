using EduHomeBackEndProject.DAL;
using EduHomeBackEndProject.Models;
using EduHomeBackEndProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackEndProject.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public CourseController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            CourseVM courseVM = new CourseVM()
            {
                Courses = _context.Courses.Include(c => c.CourseFeatures).Include(c => c.CourseTags).ThenInclude(ct => ct.Tag).Include(c => c.Category).ToList(),
            };
            return View(courseVM);
        }
        public IActionResult Details(int id)
        {
            CourseDetailVM courseDetailVM = new CourseDetailVM
            {
                Course = _context.Courses.Include(c => c.Comments).ThenInclude(cc => cc.AppUser).Include(c => c.CourseFeatures).Include(c => c.CourseTags).ThenInclude(ct => ct.Tag).Include(c => c.Category).FirstOrDefault(c => c.Id == id),
                Tags = _context.Tags.Include(t => t.CourseTags).ThenInclude(ct => ct.Course).ToList(),
                Categories = _context.Categories.Include(x => x.Courses).ToList(),
                CourseFeatures = _context.CourseFeatures.Include(cf => cf.Course).Where(cf => cf.CourseId == id).ToList(),
                Comments = _context.Comments.Include(c => c.Course).Include(c => c.AppUser).Where(c => c.CourseId == id).ToList(),


            };
            ViewBag.Courses = _context.Courses.Include(c => c.Category).Where(c => c.CategoryId == id).ToList();
            ViewBag.LastestBlogs = _context.Blogs.OrderByDescending(b => b.Date).Take(3).ToList();
            //return Json(courseDetailVM.Course.CourseName);
            return View(courseDetailVM);
        }
        [Authorize]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return RedirectToAction("Details", "Course", new { id = comment.CourseId });
            if (!_context.Courses.Any(f => f.Id == comment.CourseId)) return NotFound();
            Comment cmnt = new Comment
            {
                Message = comment.Message,
                CourseId = comment.CourseId,
                Date = DateTime.Now,
                AppUserId = user.Id,
                IsAccess = true
            };
            _context.Comments.Add(cmnt);
            _context.SaveChanges();
            return RedirectToAction("Details", "Course", new { id = comment.CourseId });
        }
        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return RedirectToAction("Index", "Course");
            if (!_context.Comments.Any(c => c.Id == id && c.IsAccess == true && c.AppUserId == user.Id)) return NotFound();
            Comment comment = _context.Comments.FirstOrDefault(c => c.Id == id && c.AppUserId == user.Id);
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return RedirectToAction("Details", "Course", new { id = comment.CourseId });
        }
        
        public IActionResult Search(string searching)
        {
            List<Course> course = _context.Courses.Where(f => f.CourseName.ToLower().Trim().Contains(searching.ToLower().Trim())).ToList();

            return PartialView("_CoursePartialView", course);
        }
        [HttpGet]
        public IActionResult Index(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                CourseVM courseVM = new CourseVM
                {
                    Courses = _context.Courses.Include(c => c.Category).Where(f => f.CourseName.ToLower().Trim().Contains(key.ToLower().Trim())).ToList()
                };
                if (!courseVM.Courses.Any(f => f.CourseName.Contains(key)))
                {
                    ModelState.AddModelError("", "No result");
                }
                return View(courseVM);
            }

            CourseVM courseV = new CourseVM
            {
                Courses = _context.Courses.Include(c => c.Category).Include(c => c.CourseTags).ThenInclude(ct => ct.Tag).ToList()
            };
            return View(courseV);
        }



        public IActionResult CategoryCourse(int Id)
        {
            List<Course> courses = _context.Courses.Where(c => c.CategoryId == Id).ToList();
            return View(courses);
        }
        public IActionResult TagCourse(int id)
        {
            List<Course> courses = _context.Courses.Include(c=>c.CourseTags).ThenInclude(ct=>ct.Tag).Where(c => c.Id== id).ToList();
            return View(courses);
        }
    }
}
