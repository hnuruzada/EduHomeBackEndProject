using EduHomeBackEndProject.DAL;
using EduHomeBackEndProject.Extensions;
using EduHomeBackEndProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EduHomeBackEndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
   

    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeacherController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Teachers.Count() / 2);
            ViewBag.CurrentPage = page;
            List<Teacher> teachers = _context.Teachers.Include(t=>t.Skills).Include(t => t.TeacherHobbies).ThenInclude(th => th.Hobbie).Include(t => t.TeacherFaculties).ThenInclude(tf => tf.Faculty).Include(t=>t.TeacherPositions).ThenInclude(tp=>tp.Position).Include(t => t.SocialMedias).Skip((page - 1) * 2).Take(2).ToList();

            return View(teachers);
        }

        public IActionResult Create()
        {
            ViewBag.Hobbies = _context.Hobbies.ToList();
            ViewBag.Faculties = _context.Faculties.ToList();
            ViewBag.Positions = _context.Positions.ToList();
            ViewBag.SocialMedias = _context.SocialMedias.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Teacher teacher)
        {
            ViewBag.Hobbies = _context.Hobbies.ToList();
            ViewBag.Faculties = _context.Faculties.ToList();
            ViewBag.Positions=_context.Positions.ToList();
            ViewBag.SocialMedias = _context.SocialMedias.ToList();


            //if (!_context.socialmedias.any(x => x.id == teacher.socialids))
            //{
            //    modelstate.addmodelerror("categoryid", "xetaniz var!");
            //    return view();
            //}
            //if (!_context.CourseTags.All(ct => ct.Id==Tag))
            //{
            //    ModelState.AddModelError("CategoryId", "Xetaniz var!");
            //    return View();
            //}

            if (!ModelState.IsValid)
            {
                return View();
            }
            teacher.TeacherPositions = new List<TeacherPosition>();
            teacher.TeacherHobbies = new List<TeacherHobbie>();

            teacher.TeacherFaculties = new List<TeacherFaculty>();
            //teacher.SocialMedias = new List<SocialMedia>();

            if (teacher.ImageFile != null)
            {
                if (!teacher.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Choose correct image format file");
                    return View();
                }
                if (!teacher.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "File must be max 2mb");
                    return View();
                }
                teacher.Image = teacher.ImageFile.SaveImg(_env.WebRootPath, "assets/img/teacher");
            }



            if (teacher.HobbieIds != null)
            {
                foreach (var hobbieId in teacher.HobbieIds)
                {
                    TeacherHobbie teacherHobbie = new TeacherHobbie
                    {
                        Teacher = teacher,
                        HobbieId = hobbieId
                    };
                    _context.TeacherHobbies.Add(teacherHobbie);
                }
            }
            if (teacher.FacultyIds != null)
            {
                foreach (var facultyId in teacher.FacultyIds)
                {
                    TeacherFaculty teacherFaculty = new TeacherFaculty
                    {
                        Teacher = teacher,
                        FacultyId = facultyId
                    };
                    _context.TeacherFaculties.Add(teacherFaculty);
                }
            }
            if (teacher.PositionIds != null)
            {
                foreach (var positionId in teacher.PositionIds)
                {
                    TeacherPosition teacherPosition = new TeacherPosition
                    {
                        Teacher = teacher,
                        PositionId = positionId
                    };
                    _context.TeacherPositions.Add(teacherPosition);
                }
            }
            
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(int id)
        {
            ViewBag.Hobbies = _context.Hobbies.ToList();
            ViewBag.Faculties = _context.Faculties.ToList();
            ViewBag.Positions = _context.Positions.ToList();
            Teacher teacher = _context.Teachers.Include(t=>t.Skills).Include(t => t.TeacherHobbies).ThenInclude(th => th.Hobbie).Include(t => t.TeacherFaculties).ThenInclude(tf => tf.Faculty).Include(t => t.TeacherPositions).ThenInclude(tp => tp.Position).Include(t => t.SocialMedias).FirstOrDefault(x => x.Id == id);
            if (teacher == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Teacher teacher)
        {
            ViewBag.Hobbies = _context.Hobbies.ToList();
            ViewBag.Faculties = _context.Faculties.ToList();
            ViewBag.Positions = _context.Positions.ToList();

            Teacher existTeacher = _context.Teachers.Include(t => t.Skills).Include(t => t.TeacherHobbies).ThenInclude(th => th.Hobbie).Include(t => t.TeacherFaculties).ThenInclude(tf => tf.Faculty).Include(t => t.TeacherPositions).ThenInclude(tp => tp.Position).Include(t => t.SocialMedias).FirstOrDefault(x => x.Id == id);
            //if (!_context.Categories.Any(x => x.Id == course.CategoryId)) return RedirectToAction(nameof(Index));
            if (existTeacher == null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (teacher.ImageFile != null)
            {
                if (!teacher.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Choose correct format file");
                    return View();
                }
                if (!teacher.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "File must be max 2mb");
                    return View();
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/teacher", existTeacher.Image);
                existTeacher.Image = teacher.ImageFile.SaveImg(_env.WebRootPath, "assets/img/teacher");


            }

            if (!ModelState.IsValid)
            {
                return View();
            }
            var existFaculties = _context.TeacherFaculties.Where(x => x.TeacherId == id).ToList();
            if (teacher.FacultyIds != null)
            {
                foreach (var facultyId in teacher.FacultyIds)
                {
                    var existFaculty = existFaculties.FirstOrDefault(x => x.FacultyId == facultyId);
                    if (existFaculty == null)
                    {
                        TeacherFaculty teacherFaculty = new TeacherFaculty
                        {
                            TeacherId = id,
                            FacultyId = facultyId,
                        };

                        _context.TeacherFaculties.Add(teacherFaculty);
                    }
                    else
                    {
                        existFaculties.Remove(existFaculty);
                    }
                }

            }
            _context.TeacherFaculties.RemoveRange(existFaculties);

            var existHobbies = _context.TeacherHobbies.Where(x => x.TeacherId == id).ToList();
            if (teacher.HobbieIds != null)
            {
                foreach (var hobbieId in teacher.HobbieIds)
                {
                    var existHobbie = existHobbies.FirstOrDefault(x => x.HobbieId == hobbieId);
                    if (existHobbie == null)
                    {
                        TeacherHobbie teacherHobbie = new TeacherHobbie
                        {
                            TeacherId = id,
                            HobbieId = hobbieId,
                        };

                        _context.TeacherHobbies.Add(teacherHobbie);
                    }
                    else
                    {
                        existHobbies.Remove(existHobbie);
                    }
                }

            }
            _context.TeacherHobbies.RemoveRange(existHobbies);

            var existPositions = _context.TeacherPositions.Where(x => x.TeacherId == id).ToList();
            if (teacher.PositionIds != null)
            {
                foreach (var positionId in teacher.PositionIds)
                {
                    var existPosition = existPositions.FirstOrDefault(x => x.PositionId == positionId);
                    if (existPosition == null)
                    {
                        TeacherPosition teacherPosition = new TeacherPosition
                        {
                            TeacherId = id,
                            PositionId = positionId,
                        };

                        _context.TeacherPositions.Add(teacherPosition);
                    }
                    else
                    {
                        existPositions.Remove(existPosition);
                    }
                }

            }

            _context.TeacherPositions.RemoveRange(existPositions);
            List<SocialMedia> existSocialMedia= _context.SocialMedias.Where(x => x.TeacherId == teacher.Id).ToList();


            List<SocialMedia> socialMedias = teacher.SocialMedias;
            if (socialMedias != null)
            {
                _context.Teachers.FirstOrDefault(x => x.Id == teacher.Id).SocialMedias = socialMedias;
            }
            if (existSocialMedia != null)
            {
                _context.SocialMedias.RemoveRange(existSocialMedia);
            }
            

            List<Skill> existSkills = _context.Skills.Where(x => x.TeacherId == teacher.Id).ToList();


            List<Skill> skills = teacher.Skills;
            if (skills != null)
            {
                _context.Teachers.FirstOrDefault(x => x.Id == teacher.Id).Skills = skills;
            }
            if (existSkills != null)
            {
                _context.Skills.RemoveRange(existSkills);
            }
            existTeacher.Name = teacher.Name;
            existTeacher.AboutMe = teacher.AboutMe;
            existTeacher.Experience = teacher.Experience;
            existTeacher.Degree = teacher.Degree;
            existTeacher.Surname = teacher.Surname;
            existTeacher.PhoneNumber = teacher.PhoneNumber;
            existTeacher.Mail = teacher.Mail;
            existTeacher.Skills=teacher.Skills;


            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {

            Teacher teacher = _context.Teachers.Include(t => t.TeacherHobbies).ThenInclude(th => th.Hobbie).Include(t => t.TeacherFaculties).ThenInclude(tf => tf.Faculty).Include(t => t.TeacherPositions).ThenInclude(tp => tp.Position).Include(t => t.SocialMedias).FirstOrDefault(hs => hs.Id == id);
            Teacher existTeacher = _context.Teachers.Include(t => t.TeacherHobbies).ThenInclude(th => th.Hobbie).Include(t => t.TeacherFaculties).ThenInclude(tf => tf.Faculty).Include(t => t.TeacherPositions).ThenInclude(tp => tp.Position).Include(t => t.SocialMedias).FirstOrDefault(s => s.Id == teacher.Id);

            if (existTeacher == null) return NotFound();
            if (teacher == null) return Json(new { status = 404 });

            Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/teacher", existTeacher.Image);

            _context.Teachers.Remove(teacher);
            _context.SaveChanges();

            return Json(new { status = 200 });

        }
    }
}
