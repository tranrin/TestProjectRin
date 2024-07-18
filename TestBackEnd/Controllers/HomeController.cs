using Microsoft.AspNetCore.Mvc;

namespace TestBackEnd.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
