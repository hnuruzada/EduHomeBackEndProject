using EduHomeBackEndProject.DAL;
using EduHomeBackEndProject.Extensions;
using EduHomeBackEndProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EduHomeBackEndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CourseController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Courses.Count() / 2);
            ViewBag.CurrentPage = page;
            List<Course> courses = _context.Courses.Include(f => f.Category).Include(c=>c.CourseTags).ThenInclude(ct=>ct.Tag).Skip((page - 1) * 2).Take(2).ToList();

            return View(courses);
        }

        public IActionResult Create()
        {
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            Course createcourse=_context.Courses.Include(c=>c.CourseTags).ThenInclude(ct=>ct.Tag).Include(c=>c.Category).FirstOrDefault(c=>c.Id==course.Id);
            ViewBag.Categories = _context.Categories.ToList();
           
            ViewBag.Tags = _context.Tags.ToList();
            if (!_context.Categories.Any(x => x.Id == course.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Xetaniz var!");
            }
          

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (course.CourseImgFile != null)
            {
                if (course.CourseImgFile.IsImage())
                {
                    ModelState.AddModelError("CourseImgFile", "Choose correct image format file");
                    return View();
                }
                if (course.CourseImgFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("CourseImgFile", "File must be max 2mb");
                    return View();
                }
                createcourse.CourseImage = course.CourseImgFile.SaveImg(_env.WebRootPath, "Assets/img/slider");
            }



            if (course.TagIds != null)
            {
                foreach (var tagId in course.TagIds)
                {
                    CourseTag courseTag = new CourseTag
                    {
                        Course = course,
                        TagId = tagId
                    };
                    _context.CourseTags.Add(courseTag);
                }
            }
            _context.Courses.Add(course);
            _context.SaveChanges();
            return RedirectToAction("index");

        }
       
    }
}
