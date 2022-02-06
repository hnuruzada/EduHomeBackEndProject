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
    [Authorize(Roles = "SuperAdmin,Admin")]

    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        public EventController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Events.Count() / 2);
            ViewBag.CurrentPage = page;
            List<Event> events = _context.Events.Include(e=>e.Comments).Include(s => s.EventSpeakers).ThenInclude(sp => sp.Speaker).Skip((page - 1) * 2).Take(2).ToList();

            return View(events);
        }

        public IActionResult Create()
        {
            ViewBag.Speakers = _context.Speakers.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event vent)
        {

            ViewBag.Speakers = _context.Speakers.ToList();


            if (!ModelState.IsValid)
            {
                return View();
            }

            if (vent.ImageFile==null)
            {
                ModelState.AddModelError("ImageFile", "Select an image");
                return View();

            }
            vent.EventSpeakers = new List<EventSpeaker>();
            if (vent.ImageFile != null)
            {
                if (!vent.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Choose correct image format file");
                    return View();
                }
                if (!vent.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "File must be max 2mb");
                    return View();
                }
                vent.Image = vent.ImageFile.SaveImg(_env.WebRootPath, "assets/img/event");
            }



            if (vent.SpeakerIds != null)
            {
                foreach (var speakerId in vent.SpeakerIds)
                {
                    EventSpeaker eventSpeaker = new EventSpeaker
                    {
                        Event = vent,
                        SpeakerId = speakerId
                    };
                    _context.EventSpeakers.Add(eventSpeaker);
                }
            }
            _context.Events.Add(vent);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(int id)
        {

            ViewBag.Speakers = _context.Speakers.ToList();
            Event vent = _context.Events.Include(x => x.EventSpeakers).ThenInclude(x => x.Speaker).FirstOrDefault(x => x.Id == id);
            if (vent == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(vent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Event vent)
        {

            ViewBag.Speakers = _context.Speakers.ToList();

            Event existEvent = _context.Events.FirstOrDefault(x => x.Id == id);
            if (existEvent == null)
            {
                return RedirectToAction("index");
            }
            if (vent.ImageFile != null)
            {
                if (!vent.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Choose correct format file");
                    return View();
                }
                if (!vent.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "File must be max 2mb");
                    return View();
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/event", existEvent.Image);
                existEvent.Image = vent.ImageFile.SaveImg(_env.WebRootPath, "assets/img/event");


            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            var existSpeakers = _context.EventSpeakers.Where(x => x.SpeakerId == id).ToList();
            if (vent.SpeakerIds != null)
            {
                foreach (var speakerId in vent.SpeakerIds)
                {
                    var existSpeaker = existSpeakers.FirstOrDefault(x => x.SpeakerId == speakerId);
                    if (existSpeaker == null)
                    {
                        EventSpeaker eventSpeaker = new EventSpeaker
                        {
                            EventId = id,
                            SpeakerId = speakerId,
                        };

                        _context.EventSpeakers.Add(eventSpeaker);
                    }
                    else
                    {
                        existSpeakers.Remove(existSpeaker);
                    }
                }

            }
            _context.EventSpeakers.RemoveRange(existSpeakers);


            existEvent.Title = vent.Title;
            existEvent.Description = vent.Description;
            existEvent.Date = vent.Date;
            existEvent.StartTime = vent.StartTime;
            existEvent.FinishTime = vent.FinishTime;
            existEvent.Venue = vent.Venue;
           




            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {

            Event vent = _context.Events.FirstOrDefault(hs => hs.Id == id);
            Event existEvent = _context.Events.FirstOrDefault(s => s.Id == vent.Id);

            if (existEvent == null) return NotFound();
            if (vent == null) return Json(new { status = 404 });

            Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/event", existEvent.Image);

            _context.Events.Remove(vent);
            _context.SaveChanges();

            return Json(new { status = 200 });


        }
        public IActionResult Comments(int eventId)
        {
            if (!_context.Comments.Any(c => c.EventId == eventId)) return RedirectToAction("Index", "Event");

            List<Comment> comments = _context.Comments.Include(c => c.Event).Include(c => c.AppUser).Where(c => c.EventId == eventId).ToList();



            return View(comments);
        }

        public IActionResult CommentStatusChange(int id)
        {
            if (!_context.Comments.Any(c => c.Id == id)) return RedirectToAction("Index", "Event");
            Comment comment = _context.Comments.SingleOrDefault(c => c.Id == id);

            comment.IsAccess = comment.IsAccess ? false : true;
            _context.SaveChanges();
            return RedirectToAction("Comments", "Event", new { EventId = comment.EventId });
        }
    }
}
