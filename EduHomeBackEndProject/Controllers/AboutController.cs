using EduHomeBackEndProject.DAL;
using EduHomeBackEndProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EduHomeBackEndProject.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        public AboutController(AppDbContext context)
        {
            _context = context; 
        }
        public IActionResult Index()
        {
            AboutVM aboutVM = new AboutVM
            {
                Settings = _context.Settings.ToList(),
                Teachers = _context.Teachers.Include(t => t.SocialMedias).Include(t => t.TeacherFaculties).ThenInclude(tf => tf.Faculty).Include(t => t.TeacherHobbies).ThenInclude(th => th.Hobbie).Include(t => t.TeacherPositions).ThenInclude(tp => tp.Position).ToList(),
                NoticeBoards=_context.NoticeBoards.ToList(),
            };
            return View(aboutVM);
        }
       
    }
}
