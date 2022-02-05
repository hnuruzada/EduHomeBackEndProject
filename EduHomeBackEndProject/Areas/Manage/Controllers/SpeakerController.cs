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
    

    public class SpeakerController : Controller
    {

        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        public SpeakerController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Speakers.Count() / 2);
            ViewBag.CurrentPage = page;
            List<Speaker> speakers = _context.Speakers.Include(s => s.SpeakerPositions).ThenInclude(sp => sp.Position).Skip((page - 1) * 2).Take(2).ToList();

            return View(speakers);
        }

        public IActionResult Create()
        {
            ViewBag.Positions = _context.Positions.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Speaker speaker)
        {

            ViewBag.Positions = _context.Positions.ToList();

            
            //if (!_context.CourseTags.All(ct => ct.Id==Tag))
            //{
            //    ModelState.AddModelError("CategoryId", "Xetaniz var!");
            //    return View();
            //}

            if (!ModelState.IsValid)
            {
                return View();
            }
            speaker.SpeakerPositions = new List<SpeakerPosition>();
            if (speaker.ImageFile != null)
            {
                if (!speaker.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Choose correct image format file");
                    return View();
                }
                if (!speaker.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "File must be max 2mb");
                    return View();
                }
                speaker.Image = speaker.ImageFile.SaveImg(_env.WebRootPath, "assets/img/event");
            }



            if (speaker.PositionIds != null)
            {
                foreach (var positionId in speaker.PositionIds)
                {
                    SpeakerPosition speakerPosition = new SpeakerPosition
                    {
                        Speaker = speaker,
                        PositionId = positionId
                    };
                    _context.SpeakerPositions.Add(speakerPosition);
                }
            }
            _context.Speakers.Add(speaker);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(int id)
        {

            ViewBag.Positions = _context.Positions.ToList();
            Speaker speaker = _context.Speakers.Include(x => x.SpeakerPositions).ThenInclude(x => x.Position).FirstOrDefault(x => x.Id == id);
            if (speaker == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(speaker);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Speaker speaker)
        {
            
            ViewBag.Positions = _context.Positions.ToList();

            Speaker existSpeaker = _context.Speakers.FirstOrDefault(x => x.Id == id);
            if (existSpeaker == null)
            {
                return RedirectToAction("index");
            }
            if (speaker.ImageFile != null)
            {
                if (!speaker.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Choose correct format file");
                    return View();
                }
                if (!speaker.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "File must be max 2mb");
                    return View();
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/event", existSpeaker.Image);
                existSpeaker.Image = speaker.ImageFile.SaveImg(_env.WebRootPath, "assets/img/event");


            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            var existPositions = _context.SpeakerPositions.Where(x => x.PositionId == id).ToList();
            if (speaker.PositionIds != null)
            {
                foreach (var positionId in speaker.PositionIds)
                {
                    var existPosition = existPositions.FirstOrDefault(x => x.PositionId == positionId);
                    if (existPosition == null)
                    {
                        SpeakerPosition speakerPosition = new SpeakerPosition
                        {
                            SpeakerId = id,
                            PositionId = positionId,
                        };

                        _context.SpeakerPositions.Add(speakerPosition);
                    }
                    else
                    {
                        existPositions.Remove(existPosition);
                    }
                }

            }
            _context.SpeakerPositions.RemoveRange(existPositions);

           
            existSpeaker.Name = speaker.Name;
            existSpeaker.Surname = speaker.Surname;
           

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {

            Speaker speaker = _context.Speakers.FirstOrDefault(hs => hs.Id == id);
            Speaker existSpeaker = _context.Speakers.FirstOrDefault(s => s.Id == speaker.Id);

            if (existSpeaker == null) return NotFound();
            if (speaker == null) return Json(new { status = 404 });

            Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/event", existSpeaker.Image);

            _context.Speakers.Remove(speaker);
            _context.SaveChanges();

            return Json(new { status = 200 });

        }
    }
}
