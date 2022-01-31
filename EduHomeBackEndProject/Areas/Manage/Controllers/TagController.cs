using EduHomeBackEndProject.DAL;
using EduHomeBackEndProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EduHomeBackEndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TagController : Controller
    {
        private readonly AppDbContext _context;
        public TagController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Tags.Count() / 2);
            ViewBag.CurrentPage = page;
            List<Tag> tags = _context.Tags.Include(t => t.CourseTags).ThenInclude(ct=>ct.Course).Skip((page - 1) * 2).Take(2).ToList();
            return View(tags);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tag tag)
        {
            List<Tag> Names = _context.Tags.Where(hs => hs.Name.ToLower().Contains(tag.Name.ToLower())).ToList();

            if (!ModelState.IsValid)
            {
                return Content("Name max 50 olmalidir");
            }
            foreach (var item in Names)
            {
                if (item.Name.ToLower().Trim().Contains(tag.Name.ToLower().Trim()))
                {
                    ModelState.AddModelError("Name", "You enter same Order.Change other Order");
                    return View(tag);
                }
            }
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            Tag tag = _context.Tags.FirstOrDefault(t => t.Id == id);
            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Tag tag,int id)
        {
            Tag Name = _context.Tags.FirstOrDefault(t => t.Name.ToLower().Trim().Contains(tag.Name.ToLower().Trim()));

            if (!ModelState.IsValid)
            {
                return View();
            }
            Tag existedTag = _context.Tags.FirstOrDefault(t => t.Id == tag.Id);
            if (existedTag == null)
            {
                return NotFound();
            }
            
            if (Name!=null && Name.Id!=id)
            {
                ModelState.AddModelError("Name", "You enter same tag.Change other tag");
                return View(existedTag);
            }
           
            existedTag.Name = tag.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Tag tag = _context.Tags.FirstOrDefault(c => c.Id == id);
            if (tag == null) return Json(new { status = 404 });

            _context.Tags.Remove(tag);
            _context.SaveChanges();

            return Json(new { status = 200 });

        }

    }
}
