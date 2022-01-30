//using EduHomeBackEndProject.Models;
using EduHomeBackEndProject.DAL;
using EduHomeBackEndProject.Models;
using EduHomeBackEndProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
                //Setting = _context.Settings.FirstOrDefault(),
            };


            
                
            
            return View(homeVM);
            
        }

       
    }
}
