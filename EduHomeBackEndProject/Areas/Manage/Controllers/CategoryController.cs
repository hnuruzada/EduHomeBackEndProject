using EduHomeBackEndProject.DAL;
using EduHomeBackEndProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EduHomeBackEndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
   

    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Categories.Count() / 2);
            ViewBag.CurrentPage = page;
            List<Category> model = _context.Categories.Include(c => c.Courses).Skip((page - 1) * 2).Take(2).ToList();

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            List<Category> Names = _context.Categories.Where(hs => hs.Name.ToLower().Contains(category.Name.ToLower())).ToList();
            if (!ModelState.IsValid)
            {
                return Content("Name max 20 olmalidir");
            }
            foreach (var item in Names)
            {
                if (item.Name.ToLower().Trim().Contains(category.Name.ToLower().Trim()))
                {
                    ModelState.AddModelError("Name", "You enter same Category Name.Write different category name!");
                    return View(category);
                }
            }

            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category, int id)
        {
            Category sameName = _context.Categories.FirstOrDefault(c => c.Name.ToLower().Trim() == category.Name.ToLower().Trim());

            if (!ModelState.IsValid)
            {
                return View();
            }
            Category existedCategory = _context.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (existedCategory == null)
            {
                return NotFound();
            }

            if (sameName != null && sameName.Id != id)
            {
                ModelState.AddModelError("Name", "You enter same tag.Change other tag");
                return View(existedCategory);
            }
            existedCategory.Name = category.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return Json(new { status = 404 });

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Json(new { status = 200 });

        }
    }
}
