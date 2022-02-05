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
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BlogController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(int page=1)
        {
            
            BlogVM blogVM = new BlogVM
            {
                Blogs = _context.Blogs.Include(b=>b.Comments).Skip((page - 1) * 4).Take(4).ToList(),
                
            };
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Blogs.Count() / 4);
            ViewBag.CurrentPage = page;
            ViewBag.LastestBlogs = _context.Blogs.OrderByDescending(b => b.Date).Take(3).ToList();



            return View(blogVM);
        }
        //[HttpGet]
        //public async Task<IActionResult> Index(string searchString, int page = 1)
        //{
        //    ViewData["GetBlogs"] = searchString;
        //    var blogQuery = from x in _context.Blogs select x;
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        blogQuery = blogQuery.Where(x => x.Description.Contains(searchString));
        //        return View(await blogQuery.AsNoTracking().ToListAsync());
        //    }
        //    else
        //    {
        //        ViewBag.PageCount = Decimal.Ceiling((decimal)_context.Blogs.Count() / 3);
        //        ViewBag.page = page;

        //        //List<BlogVM> blogs = _context.Blogs.Skip(3).ToList();

        //        List<Blog> blogs1 = _context.Blogs.Skip((int)(page - 1) * 3).Take(3).ToList();
        //        return View(blogs1);
        //    }
        //}
        public IActionResult Details(int id) 
        {
            BlogDetailVM detailVM = new BlogDetailVM
            {
                Blog = _context.Blogs.Include(b=>b.Comments).ThenInclude(b=>b.AppUser).FirstOrDefault(b=>b.Id==id),
                Comments = _context.Comments.Include(c=>c.Blog).Include(c=>c.AppUser).Where(c=>c.BlogId==id).ToList(),
            };
            ViewBag.LastestBlogs = _context.Blogs.OrderByDescending(b => b.Date).Take(3).ToList();
            return View(detailVM);
        
        }
        [Authorize]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return RedirectToAction("Details", "Blog", new { id = comment.BlogId });
            if (!_context.Blogs.Any(f => f.Id == comment.BlogId)) return NotFound();
            Comment cmnt = new Comment
            {
                Message = comment.Message,
              
                BlogId = comment.BlogId,
                Date = DateTime.Now,
                AppUserId = user.Id,
                 IsAccess = true,
            };
            _context.Comments.Add(cmnt);
            _context.SaveChanges();
            return RedirectToAction("Details", "Blog", new { id = comment.BlogId });
        }
        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return RedirectToAction("Index", "Blog");
            if (!_context.Comments.Any(c => c.Id == id && c.IsAccess == true && c.AppUserId == user.Id)) return NotFound();
            Comment comment = _context.Comments.FirstOrDefault(c => c.Id == id && c.AppUserId == user.Id);
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return RedirectToAction("Details", "Blog", new { id = comment.BlogId });
        }
        public IActionResult Search(string searching)
        {
            List<Blog> blog = _context.Blogs.Where(f => f.Title.ToLower().Trim().Contains(searching.ToLower().Trim())).ToList();

            return PartialView("_BlogPartialView", blog);
        }

    }
}
