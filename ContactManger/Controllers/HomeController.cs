using Microsoft.AspNetCore.Mvc;

namespace ContactManger.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
