using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace DAPM.Controllers
{
    public class HomeController : Controller
    {
        private readonly Data.DbLuLutHoaVangContext _context;

        public HomeController(Data.DbLuLutHoaVangContext context)
        {
            _context = context;
        }
        private readonly ILogger? _logger;
        
        public IActionResult Login()
        {
            return View();
        }

        [Route("Home/Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string usename, string password)
        {
            if (ModelState.IsValid)
            {
                var data = _context.TbTaiKhoans.Where(s => s.TenDangNhap.Equals(usename) && s.MatKhau.Equals(password)).ToList();
                if (data.Count() > 0)
                {
                    if (data[0].Quyen == "admin")
                    {
                        return RedirectToAction("Index", "TaiKhoans");
                    } else if (data[0].Quyen == "subadmin")
                    {
                        return RedirectToAction("Index");
                    }
                    
                }
                else
                {
                    TempData["Message"] = "Đăng nhập không thành công! Vui lòng kiểm tra lại tên đăng nhập và mật khẩu.";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
    }
}
