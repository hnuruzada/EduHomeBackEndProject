using Microsoft.AspNetCore.Mvc;

namespace EduHomeBackEndProject.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
