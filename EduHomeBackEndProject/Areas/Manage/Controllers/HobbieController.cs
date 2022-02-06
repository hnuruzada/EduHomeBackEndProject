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

    [Authorize(Roles = "SuperAdmin,Admin")]
    public class HobbieController : Controller
    {
        private readonly AppDbContext _context;
        public HobbieController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Hobbies.Count() / 2);
            ViewBag.CurrentPage = page;
            List<Hobbie> model = _context.Hobbies.Include(c => c.TeacherHobbies).ThenInclude(th=>th.Teacher).Skip((page - 1) * 2).Take(2).ToList();

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Hobbie hobbie)
        {
            List<Hobbie> Names = _context.Hobbies.Where(hs => hs.Name.ToLower()==hobbie.Name.ToLower()).ToList();
            if (!ModelState.IsValid)
            {
                return Content("Name max 20 olmalidir");
            }
            foreach (var item in Names)
            {
                if (item.Name.ToLower().Trim().Contains(hobbie.Name.ToLower().Trim()))
                {
                    ModelState.AddModelError("Name", "You enter same hobbie.Write different hobbie name!");
                    return View(hobbie);
                }
            }

            _context.Hobbies.Add(hobbie);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            Hobbie hobbie = _context.Hobbies.FirstOrDefault(c => c.Id == id);
            return View(hobbie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Hobbie hobbie, int id)
        {
            Hobbie sameName = _context.Hobbies.FirstOrDefault(c => c.Name.ToLower().Trim() == hobbie.Name.ToLower().Trim());

            if (!ModelState.IsValid)
            {
                return View();
            }
            Hobbie existHobbie = _context.Hobbies.FirstOrDefault(c => c.Id == hobbie.Id);
            if (existHobbie == null)
            {
                return NotFound();
            }

            if (sameName != null && sameName.Id != id)
            {
                ModelState.AddModelError("Name", "You enter same hobbie.Change other hobbie");
                return View(existHobbie);
            }
            existHobbie.Name = hobbie.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Hobbie hobbie = _context.Hobbies.FirstOrDefault(c => c.Id == id);
            if (hobbie == null) return Json(new { status = 404 });

            _context.Hobbies.Remove(hobbie);
            _context.SaveChanges();

            return Json(new { status = 200 });

        }
    }
}
