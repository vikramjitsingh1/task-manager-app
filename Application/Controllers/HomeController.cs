using Microsoft.AspNetCore.Mvc;

namespace TmModule.Application.CoreController
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
