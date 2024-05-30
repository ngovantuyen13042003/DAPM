using Microsoft.AspNetCore.Mvc;

namespace DAPM.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Product()
        {
            return View();
        }
    }
}
