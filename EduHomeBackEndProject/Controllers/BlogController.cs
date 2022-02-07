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
        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Blogs.Count() / 4);
            ViewBag.CurrentPage = page;
            BlogVM blogVM = new BlogVM
            {
                Blogs = _context.Blogs.Include(b => b.Comments).Skip((page - 1) * 4).Take(4).ToList(),

            };

            ViewBag.LastestBlogs = _context.Blogs.Include(b=>b.Comments).OrderByDescending(b => b.Date).Take(3).ToList();



            return View(blogVM);
        }

        public IActionResult Details(int id)
        {
            BlogDetailVM detailVM = new BlogDetailVM
            {
                Blog = _context.Blogs.Include(b => b.Comments).ThenInclude(b => b.AppUser).FirstOrDefault(b => b.Id == id),
                Comments = _context.Comments.Include(c => c.Blog).Include(c => c.AppUser).Where(c => c.BlogId == id).ToList(),
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
            List<Blog> blog = _context.Blogs.Include(b => b.Comments).Where(f => f.Title.ToLower().Trim().Contains(searching.ToLower().Trim())).ToList();

            return PartialView("_BlogPartialView", blog);
        }
        [HttpGet]
        public IActionResult Index(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                BlogVM blogVMs = new BlogVM
                {
                    Blogs = _context.Blogs.Include(b => b.Comments).Where(f => f.Title.ToLower().Trim().Contains(key.ToLower().Trim())).ToList()
                };
                if (!blogVMs.Blogs.Any(f => f.Title.Contains(key)))
                {
                    ModelState.AddModelError("", "No result");
                }
                return View(blogVMs);
            }

            BlogVM blogV = new BlogVM
            {
                Blogs = _context.Blogs.Include(b => b.Comments).ToList()
            };
            ViewBag.LastestBlogs = _context.Blogs.Include(b => b.Comments).OrderByDescending(b => b.Date).Take(3).ToList();

            return View(blogV);
        }

    }
}

