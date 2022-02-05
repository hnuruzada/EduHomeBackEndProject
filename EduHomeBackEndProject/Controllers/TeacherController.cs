using EduHomeBackEndProject.DAL;
using EduHomeBackEndProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EduHomeBackEndProject.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        public TeacherController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            TeacherVM teacherVM = new TeacherVM
            {
                Teachers = _context.Teachers.Include(t => t.SocialMedias).Include(t => t.TeacherFaculties).ThenInclude(tf => tf.Faculty).Include(t => t.TeacherHobbies).ThenInclude(th => th.Hobbie).Include(t => t.TeacherPositions).ThenInclude(tp => tp.Position).ToList(),
            };
            
        return View(teacherVM);
        }
        public IActionResult Details(int id)
        {
            TeacherDetailVM teacherDetailVM = new TeacherDetailVM
            {
                Teacher =_context.Teachers.Include(t => t.SocialMedias).Include(t => t.TeacherFaculties).ThenInclude(tf => tf.Faculty).Include(t => t.TeacherHobbies).ThenInclude(th => th.Hobbie).Include(t => t.TeacherPositions).ThenInclude(tp => tp.Position).FirstOrDefault(t=>t.Id == id),
                TeacherFaculties=_context.TeacherFaculties.Include(tf=>tf.Teacher).Include(tf=>tf.Faculty).Where(tf=>tf.TeacherId==id).ToList(),
                TeacherHobbies=_context.TeacherHobbies.Include(th=>th.Teacher).Include(th=>th.Hobbie).Where(th=>th.TeacherId==id).ToList(),
                TeacherPositions=_context.TeacherPositions.Include(tp=>tp.Position).Include(tp=>tp.Teacher).ToList(),
                Skills=_context.Skills.Include(s=>s.Teacher).Where(s=>s.TeacherId==id).ToList(),
                SocialMedias=_context.SocialMedias.Include(sm=>sm.Teacher).Where(sm=>sm.TeacherId==id).ToList(),    
                
            };
            

            return View(teacherDetailVM);
        }
    }
}
