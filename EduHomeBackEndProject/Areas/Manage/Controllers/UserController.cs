using EduHomeBackEndProject.DAL;
using EduHomeBackEndProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackEndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public UserController(UserManager<AppUser> userManager,AppDbContext context)
        {
            _context = context;
            _userManager = userManager;
             
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Users.Count() / 6);
            ViewBag.CurrentPage = page;
            List<AppUser> user = _userManager.Users.Skip((page - 1) * 6).Take(6).ToList();
            
            return View(user);
        }
        public async Task<IActionResult> UserStatusChange(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
           
            user.IsAdmin = user.IsAdmin ? false : true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
