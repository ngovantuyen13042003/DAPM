using DAPM.Data;
using DAPM.Models;
using DAPM.Services;
using DAPM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using X.PagedList;

namespace DAPM.Controllers
{
    public class UngHoController : Controller
    {
        private readonly UngHoService ungHoService;
        private readonly DotLuService dotLuService;
        private readonly DanhMucService danhMucService;
        private readonly DbLuLutHoaVangContext context;

        public UngHoController(UngHoService ungHoService, DbLuLutHoaVangContext context, DotLuService dotLuService, DanhMucService danhMucService)
        {
            this.ungHoService = ungHoService;
            this.context = context;
            this.dotLuService = dotLuService;
            this.danhMucService = danhMucService;
        }

        public IActionResult Index(int? page, string filter)
        {
            int pageSize = 6;
            int pageNumber = page ?? 1;

            var list = ungHoService.filter(filter ?? "all");

            ViewBag.CurrentFilter = filter;

            PagedList<DonDangKyViewModel> pagination = new PagedList<DonDangKyViewModel> (list, pageNumber, pageSize);

            return View(pagination);
        }

        public IActionResult update(long? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }else
            {
                ungHoService.ApproveUngHo(id);
            }

            return RedirectToAction("Index", "UngHo");
        }

        [HttpGet]
        public IActionResult AddDonDK()
        {
            var dl = dotLuService.GetAll();
            var dm = danhMucService.getAll();
            var data = new  Tuple< List<TbDanhMuc>, List<TbDotLu> >(dm, dl);
            return View(data);
        }

        [HttpPost]
        public IActionResult AddDonDK(TbDonDangKyUngHo tbDonDangKyUngHo)
        {
            ungHoService.Add(tbDonDangKyUngHo);
            return RedirectToAction("Index");
        }
        


    }
}
