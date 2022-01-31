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
            ViewBag.Categories = _context.Categories.ToList();
           
            ViewBag.Tags = _context.Tags.ToList();

            if (!_context.Categories.Any(x => x.Id == course.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Xetaniz var!");
                return View();
            }
            //if (!_context.CourseTags.All(ct => ct.Id==Tag))
            //{
            //    ModelState.AddModelError("CategoryId", "Xetaniz var!");
            //    return View();
            //}

            if (!ModelState.IsValid)
            {
                return View();
            }
            course.CourseTags = new List<CourseTag>();
            if (course.CourseImgFile != null)
            {
                if (!course.CourseImgFile.IsImage())
                {
                    ModelState.AddModelError("CourseImgFile", "Choose correct image format file");
                    return View();
                }
                if (!course.CourseImgFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("CourseImgFile", "File must be max 2mb");
                    return View();
                }
                course.CourseImage = course.CourseImgFile.SaveImg(_env.WebRootPath, "assets/img/course");
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
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(int id)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            Course course = _context.Courses.Include(x => x.CourseTags).Include(x => x.CourseFeatures).FirstOrDefault(x => x.Id == id);
            if (course == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Course course)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Tags = _context.Tags.ToList();

            Course existCourse = _context.Courses.FirstOrDefault(x => x.Id == id);
            if (!_context.Categories.Any(x => x.Id == course.CategoryId)) return RedirectToAction(nameof(Index));
            if (existCourse == null)
            {
                return RedirectToAction("index");
            }
            if (course.CourseImgFile != null)
            {
                if (course.CourseImgFile.IsImage())
                {
                    ModelState.AddModelError("CourseImgFile", "Choose correct format file");
                    return View();
                }
                if (course.CourseImgFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("CourseImgFile", "File must be max 2mb");
                    return View();
                }

                existCourse.CourseImage = course.CourseImgFile.SaveImg(_env.WebRootPath, "assets/img/course");

                
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            var existTags = _context.CourseTags.Where(x => x.CourseId == id).ToList();
            if (course.TagIds != null)
            {
                foreach (var tagId in course.TagIds)
                {
                    var existTag = existTags.FirstOrDefault(x => x.TagId == tagId);
                    if (existTag == null)
                    {
                        CourseTag courseTag = new CourseTag
                        {
                            CourseId = id,
                            TagId = tagId,
                        };
                        
                        _context.CourseTags.Add(courseTag);
                    }
                    else
                    {
                        existTags.Remove(existTag);
                    }
                }

            }
            _context.CourseTags.RemoveRange(existTags);

            List<CourseFeature> existFeatures = _context.CourseFeatures.Where(x => x.CourseId == course.Id).ToList();


            List<CourseFeature> features = course.CourseFeatures;
            if (features != null)
            {
                _context.Courses.FirstOrDefault(x => x.Id == course.Id).CourseFeatures = features;
            }
            if (existFeatures != null)
            {
                _context.CourseFeatures.RemoveRange(existFeatures);
            }
            existCourse.CourseName = course.CourseName;
            existCourse.AboutCourseInfo = course.AboutCourseInfo;
            existCourse.HowToApplyInfo = course.HowToApplyInfo;
            existCourse.CertificationInfo = course.CertificationInfo;
            existCourse.Price = course.Price;
            existCourse.CourseFeatures = course.CourseFeatures;

            existCourse.CategoryId = course.CategoryId;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {

            Course course = _context.Courses.FirstOrDefault(hs => hs.Id == id);
            Course existCourse = _context.Courses.FirstOrDefault(s => s.Id == course.Id);

            if (existCourse == null) return NotFound();
            if (course == null) return Json(new { status = 404 });

            Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/course", existCourse.CourseImage);

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return Json(new { status = 200 });

        }

    }
}
