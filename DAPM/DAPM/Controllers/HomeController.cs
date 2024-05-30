using Microsoft.AspNetCore.Mvc;

namespace DAPM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger? _logger;
        public IActionResult Index()
        {
            return View();
        }
    }
}
