using EduHomeBackEndProject.DAL;
using EduHomeBackEndProject.Extensions;
using EduHomeBackEndProject.Models;
using EduHomeBackEndProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackEndProject.Areas.Manage.Controllers
{
    [Area("Manage")]

    [Authorize(Roles = "SuperAdmin")]

    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BlogController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Blogs.Count() / 2);
            ViewBag.CurrentPage = page;
            List<Blog> model = _context.Blogs.Include(b=>b.Comments).Skip((page - 1) * 2).Take(2).ToList();
            
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Blog blog)
        {
            //return Json(blog.ImageFile.FileName.ToString());

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (blog.ImageFile != null)
            {
                if (!blog.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Choose correct image format file");
                    return View();
                }
                if (!blog.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "File must be max 2mb");
                    return View();
                }
                blog.Image = blog.ImageFile.SaveImg(_env.WebRootPath, "assets/img/blog");
            }
            //else
            //{
            //    ModelState.AddModelError("ImageFile", "Select Image");
            //    return View();
            //}

            _context.Blogs.Add(blog);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            Blog blog = _context.Blogs.FirstOrDefault(c => c.Id == id);
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Blog blog, int id)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }
            Blog existedBlog = _context.Blogs.FirstOrDefault(c => c.Id == blog.Id);
            if (existedBlog == null)
            {
                return NotFound();
            }
            if (blog.ImageFile != null)
            {
                if (!blog.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Choose correct format file");
                    return View();
                }
                if (!blog.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "File must be max 2mb");
                    return View();
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/blog", existedBlog.Image);
                existedBlog.Image = blog.ImageFile.SaveImg(_env.WebRootPath, "assets/img/blog");


            }

            existedBlog.Title = blog.Title;
            existedBlog.Date = blog.Date;
            existedBlog.Description = existedBlog.Description;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Blog blog = _context.Blogs.FirstOrDefault(c => c.Id == id);
            if (blog == null) return Json(new { status = 404 });

            Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/blog", blog.Image);

            _context.Blogs.Remove(blog);
            _context.SaveChanges();

            return Json(new { status = 200 });

        }

        
        public IActionResult Comments(int blogId)
        {
            if (!_context.Comments.Any(c => c.BlogId == blogId)) return RedirectToAction("Index", "Blog");

            List<Comment> comments = _context.Comments.Include(c => c.Blog).Include(c => c.AppUser).Where(c => c.BlogId == blogId).ToList();
            
                
           
            return View(comments);
        }
        
        public IActionResult CommentStatusChange(int id)
        {
            if (!_context.Comments.Any(c => c.Id == id)) return RedirectToAction("Index", "Blog");
            Comment comment = _context.Comments.SingleOrDefault(c => c.Id == id);
            
            comment.IsAccess = comment.IsAccess ? false : true;
            _context.SaveChanges();
            return RedirectToAction("Comments", "Blog", new { BlogId = comment.BlogId });
        }

    }
}
