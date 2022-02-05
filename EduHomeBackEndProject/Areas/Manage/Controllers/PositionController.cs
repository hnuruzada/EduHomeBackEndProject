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
   

    public class PositionController : Controller
    {
        private readonly AppDbContext _context;
        public PositionController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Positions.Count() / 2);
            ViewBag.CurrentPage = page;
            List<Position> position = _context.Positions.Include(p => p.TeacherPositions).ThenInclude(tp => tp.Teacher).Include(p=>p.SpeakerPositions).ThenInclude(sp=>sp.Speaker).Include(p=>p.Students).Skip((page - 1) * 2).Take(2).ToList();
            return View(position);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Position position)
        {
            List<Position> Names = _context.Positions.Where(hs => hs.Name.ToLower()==position.Name.ToLower()).ToList();

            if (!ModelState.IsValid)
            {
                return Content("Name max 50 olmalidir");
            }
            foreach (var item in Names)
            {
                if (item.Name.ToLower().Trim().Contains(position.Name.ToLower().Trim()))
                {
                    ModelState.AddModelError("Name", "You enter same Tag name.Write different Nsme!");
                    return View(position);
                }
            }
            _context.Positions.Add(position);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            Position position = _context.Positions.FirstOrDefault(t => t.Id == id);
            return View(position);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Position position, int id)
        {
            if (position.Name == null)
            {
                ModelState.AddModelError("Name", "You enter Name");
                return View(position);
            }
            Position Name = _context.Positions.FirstOrDefault(t => t.Name.ToLower().Trim() == position.Name.ToLower().Trim());


            if (!ModelState.IsValid)
            {
                return View();
            }
            Position existedPosition = _context.Positions.FirstOrDefault(t => t.Id == position.Id);
            if (existedPosition == null)
            {
                return NotFound();
            }


            if (Name != null && Name.Id != id)
            {
                ModelState.AddModelError("Name", "You enter same position.Change other position");
                return View(existedPosition);
            }


            existedPosition.Name = position.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Position position = _context.Positions.FirstOrDefault(c => c.Id == id);
            if (position == null) return Json(new { status = 404 });

            _context.Positions.Remove(position);
            _context.SaveChanges();

            return Json(new { status = 200 });

        }

    }
}
