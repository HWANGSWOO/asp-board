using Microsoft.AspNetCore.Mvc;

namespace asp.Controllers
{
    public class StudyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
