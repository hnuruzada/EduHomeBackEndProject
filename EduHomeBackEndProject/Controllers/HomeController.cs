//using EduHomeBackEndProject.Models;
using EduHomeBackEndProject.DAL;
using EduHomeBackEndProject.Models;
using EduHomeBackEndProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackEndProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                HeaderSliders = _context.HeaderSliders.OrderBy(x => x.Order).ToList(),
                Settings = _context.Settings.ToList(),
                Courses=_context.Courses.Include(c=>c.CourseFeatures).Include(c=>c.CourseTags).ThenInclude(ct=>ct.Tag).Include(c=>c.Category).Take(3).ToList(),
                Events=_context.Events.Include(e=>e.EventSpeakers).ThenInclude(ep=>ep.Speaker).ToList(),
                NoticeBoards=_context.NoticeBoards.ToList(),
                Blogs=_context.Blogs.Include(b => b.Comments).ToList(),
            };


            
                
            
            return View(homeVM);
            
        }

       
    }
}
