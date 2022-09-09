using Microsoft.AspNetCore.Mvc;

namespace YET.Controllers
{
    public class TestingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
