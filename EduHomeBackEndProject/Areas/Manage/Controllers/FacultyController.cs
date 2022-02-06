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
    public class FacultyController : Controller
    {
        private readonly AppDbContext _context;
        public FacultyController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Faculties.Count() / 2);
            ViewBag.CurrentPage = page;
            List<Faculty> faculties = _context.Faculties.Include(t => t.TeacherFaculties).ThenInclude(ct => ct.Teacher).Skip((page - 1) * 2).Take(2).ToList();
            return View(faculties);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Faculty faculty)
        {
            List<Faculty> facultyName = _context.Faculties.Where(hs => hs.FacultyName.ToLower().Trim().Contains(faculty.FacultyName.ToLower().Trim())).ToList();
            

            if (!ModelState.IsValid)
            {
                return Content("Name max 50 olmalidir");
            }
            foreach (var item in facultyName)
            {
                if (item.FacultyName.ToLower().Trim().Contains(faculty.FacultyName.ToLower().Trim()))
                {
                    ModelState.AddModelError("FacultyName", "You enter same Faculty Name.Write other Name!");
                    return View(faculty);
                }
               
            }
           
            _context.Faculties.Add(faculty);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            Faculty faculty = _context.Faculties.FirstOrDefault(f => f.Id == id);
            return View(faculty);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Faculty faculty, int id)
        {
            Faculty Name = _context.Faculties.FirstOrDefault(t => t.FacultyName.ToLower().Trim()==faculty.FacultyName.ToLower().Trim());
            if (!ModelState.IsValid)
            {
                return View();
            }
            Faculty existedFaculty = _context.Faculties.FirstOrDefault(t => t.Id == faculty.Id);
            if (existedFaculty == null)
            {
                return NotFound();
            }

            if (Name != null && Name.Id != id)
            {
                ModelState.AddModelError("FacultyName", "You enter same Faculty name.Change other Name!");
                return View(existedFaculty);
            }
           
            existedFaculty.FacultyName = faculty.FacultyName;
            existedFaculty.DepartmentName = faculty.DepartmentName;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Faculty faculty = _context.Faculties.FirstOrDefault(c => c.Id == id);
            if (faculty == null) return Json(new { status = 404 });

            _context.Faculties.Remove(faculty);
            _context.SaveChanges();

            return Json(new { status = 200 });

        }
    }
}
