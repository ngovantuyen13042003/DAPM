using Azure;
using DAPM.Data;
using DAPM.Models;
using DAPM.Services;
using DAPM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using X.PagedList;

namespace DAPM.Controllers
{
    public class ProductController : Controller
    {
        private readonly HangHoaService hangHoaService;
        private readonly DanhMucService danhMucService;
        private readonly DbLuLutHoaVangContext mydb;

        public ProductController(HangHoaService hangHoaService, DanhMucService danhMucService, DbLuLutHoaVangContext mydb)
        {
            this.hangHoaService = hangHoaService;
            this.danhMucService = danhMucService;
            this.mydb = mydb;
        }


        public IActionResult Product(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var listHangHoa = mydb.TbHangHoas
                                .Include(hh => hh.IdDanhMucNavigation)
                                .OrderBy(hh => hh.IdHangHoa)
                                .ToList();

            var listfullhh = listHangHoa.Select(hh => new HangHoaViewModel
            {
                IdHangHoa = hh.IdHangHoa,
                TenHangHoa = hh.TenHangHoa,
                MoTa = hh.MoTa,
                SoLuong = hh.SoLuong,
                DonViTinh = hh.DonViTinh,
                IdDanhMuc = hh.IdDanhMuc,
                TenDanhMuc = hh.IdDanhMucNavigation != null ? hh.IdDanhMucNavigation.TenDanhMuc : null
            }).ToList();

            PagedList<HangHoaViewModel> pagination = new PagedList<HangHoaViewModel>(listfullhh, pageNumber, pageSize);

            return View(pagination);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            var data = danhMucService.getAll();
            return View(data);
        }
        [HttpPost]
        public IActionResult AddProduct(TbHangHoa hanghoa) 
        {
            hangHoaService.Add(hanghoa);
            return RedirectToAction("Product");
        }

        [HttpGet]
        public IActionResult EditProduct(int id) 
        { 
            List<TbDanhMuc> dm = danhMucService.getAll();
            TbHangHoa hh = hangHoaService.getById(id);
            var data =new Tuple<TbHangHoa, List<TbDanhMuc>>(hh, dm);
            return View(data);
        }

        [HttpPost]
        public IActionResult EditProduct(TbHangHoa hanghoa)
        {
            hangHoaService.Edit(hanghoa);
            return RedirectToAction("Product");
        }

        [HttpGet]
        [Route("Product/DeleteProduct")]
        public IActionResult DeleteProduct(long id)
        {
            TempData["Message"] = "";
            TbHangHoa hh = hangHoaService.getById(id);
            if(hh == null)
            {
                TempData["Message"] = "Not found product with id = " + id;
            }
            else
            {
                hangHoaService.Delete(hh);
                TempData["Message"] = "Delete success product with id = " + id;
            }
          
            return RedirectToAction("Product", "Product");
        }
    }
}
