using Microsoft.AspNetCore.Mvc;

namespace EduHomeBackEndProject.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
