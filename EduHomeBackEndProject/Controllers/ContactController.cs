using Microsoft.AspNetCore.Mvc;

namespace EduHomeBackEndProject.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
