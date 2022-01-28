using Microsoft.AspNetCore.Mvc;

namespace EduHomeBackEndProject.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
