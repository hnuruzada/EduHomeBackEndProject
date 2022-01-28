using Microsoft.AspNetCore.Mvc;

namespace EduHomeBackEndProject.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
