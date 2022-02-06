using EduHomeBackEndProject.DAL;
using EduHomeBackEndProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EduHomeBackEndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]

    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ContactController(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            List<Contact> contact = _context.Contacts.ToList();           
            return View(contact);
        }
      
        public IActionResult Delete(int id)
        {
            Contact contact = _context.Contacts.SingleOrDefault(c => c.Id == id);
            if (contact == null) return Json(new { status = 404 });

            _context.Contacts.Remove(contact);
            _context.SaveChanges();

            return Json(new { status = 200 });

        }
    }
}
