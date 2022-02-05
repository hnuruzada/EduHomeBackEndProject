using EduHomeBackEndProject.DAL;
using EduHomeBackEndProject.Models;
using EduHomeBackEndProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackEndProject.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public EventController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            EventVM eventVM = new EventVM
            {
                Events = _context.Events.Include(e => e.EventSpeakers).ThenInclude(ep => ep.Speaker).ToList(),

            };
            return View(eventVM);
        }
        public IActionResult Details(int id)
        {
            EventDetailVM eventDetailVM = new EventDetailVM
            {
                Event = _context.Events.Include(x => x.EventSpeakers).ThenInclude(es => es.Speaker).FirstOrDefault(x => x.Id == id),
                EventSpeakers = _context.EventSpeakers.Include(es => es.Event).Include(es => es.Speaker).Where(es=>es.EventId==id).ToList(),
                Speakers=_context.Speakers.Include(s=>s.EventSpeakers).ThenInclude(es=>es.Event).Include(s=>s.SpeakerPositions).ThenInclude(sp=>sp.Position).Where(s=>s.Id==id).ToList(),
                Comments=_context.Comments.Include(c=>c.Event).Include(c=>c.AppUser).Where(c=>c.EventId==id).ToList(),
                Events=_context.Events.Include(e=>e.EventSpeakers).ThenInclude(es=>es.Speaker).Where(e=>e.Id==id).ToList()
            };

            ViewBag.LastestBlogs = _context.Blogs.OrderByDescending(b => b.Date).Take(3).ToList();
            return View(eventDetailVM);
        }
        [Authorize]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return RedirectToAction("Details", "Event", new { id = comment.EventId });
            if (!_context.Events.Any(f => f.Id == comment.EventId)) return NotFound();
            Comment cmnt = new Comment
            {
                Message = comment.Message,
                EventId = comment.EventId,
                Date = DateTime.Now,
                AppUserId = user.Id,
                IsAccess = true,

            };
            _context.Comments.Add(cmnt);
            _context.SaveChanges();
            return RedirectToAction("Details", "Event", new { id = comment.EventId });
        }
        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return RedirectToAction("Index", "Event");
            if (!_context.Comments.Any(c => c.Id == id && c.IsAccess == true && c.AppUserId == user.Id)) return NotFound();
            Comment comment = _context.Comments.FirstOrDefault(c => c.Id == id && c.AppUserId == user.Id);
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return RedirectToAction("Details", "Event", new { id = comment.EventId });
        }
        public IActionResult Search(string searching)
        {
            List<Event> sevent = _context.Events.Where(f => f.Title.ToLower().Trim().Contains(searching.ToLower().Trim())).ToList();

            return PartialView("_EventPartialView", sevent);
        }
    }
}
