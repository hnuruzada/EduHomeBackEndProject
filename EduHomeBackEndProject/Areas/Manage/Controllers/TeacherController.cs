using Microsoft.AspNetCore.Mvc;

namespace EduHomeBackEndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
