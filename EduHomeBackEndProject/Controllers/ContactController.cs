using EduHomeBackEndProject.DAL;
using EduHomeBackEndProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EduHomeBackEndProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ContactController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Message(Contact msg)
        {
            if (!ModelState.IsValid) return View();
            Contact contact = new Contact
            {
                Id = msg.Id,
                Message = msg.Message,
                Email = msg.Email,
                Date = DateTime.Now,
            };
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    }
}
