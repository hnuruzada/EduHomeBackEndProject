using EduHomeBackEndProject.DAL;
using EduHomeBackEndProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EduHomeBackEndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    

    public class NoticeBoardController : Controller
    {
        private readonly AppDbContext _context;
        public NoticeBoardController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.NoticeBoards.Count() / 2);
            ViewBag.CurrentPage = page;
            List<NoticeBoard> model = _context.NoticeBoards.Skip((page - 1) * 2).Take(2).ToList();

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NoticeBoard noticeBoard)
        {
            
            if (!ModelState.IsValid)
            {
                return Content("Name max 20 olmalidir");
            }
           

            _context.NoticeBoards.Add(noticeBoard);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            NoticeBoard noticeBoard = _context.NoticeBoards.FirstOrDefault(c => c.Id == id);
            return View(noticeBoard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NoticeBoard noticeBoard, int id)
        {
           

            if (!ModelState.IsValid)
            {
                return View();
            }
            NoticeBoard existedNoticeBoard = _context.NoticeBoards.FirstOrDefault(c => c.Id == noticeBoard.Id);
            if (existedNoticeBoard == null)
            {
                return NotFound();
            }

          
            existedNoticeBoard.Date = noticeBoard.Date;
            existedNoticeBoard.Description = noticeBoard.Description;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            NoticeBoard noticeBoard= _context.NoticeBoards.FirstOrDefault(c => c.Id == id);
            if (noticeBoard == null) return Json(new { status = 404 });

            _context.NoticeBoards.Remove(noticeBoard);
            _context.SaveChanges();

            return Json(new { status = 200 });

        }
    }
}
