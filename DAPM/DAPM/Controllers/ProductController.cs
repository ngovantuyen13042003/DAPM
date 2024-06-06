using Azure;
using DAPM.Data;
using DAPM.Models;
using DAPM.Services;
using DAPM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace DAPM.Controllers
{
    public class ProductController : Controller
    {
        private readonly HangHoaService hangHoaService;
        private readonly DanhMucService danhMucService;
        private readonly DbLuLutHoaVangContext mydb;
        private readonly DonDKService donDKService;
        private readonly ChiTietHangUngHoService chiTietHangUngService;

        public ProductController(HangHoaService hangHoaService, DanhMucService danhMucService, DbLuLutHoaVangContext mydb, DonDKService donDKService, ChiTietHangUngHoService chiTietHangUngService)
        {
            this.hangHoaService = hangHoaService;
            this.danhMucService = danhMucService;
            this.mydb = mydb;
            this.donDKService = donDKService;
            this.donDKService = donDKService;
            this.chiTietHangUngService = chiTietHangUngService;
            this.chiTietHangUngService = chiTietHangUngService;
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
            var dm = danhMucService.getAll();
            var donDK = donDKService.getAll();

            var data = new Tuple<List<TbDanhMuc>, List<DonDangKyViewModel>>(dm, donDK);

            return View(data);
        }
        [HttpPost]
        public  IActionResult  AddProduct(HangHoaViewModel hanghoa) 
        {
            TbHangHoa hh = new TbHangHoa();
            hh.IdHangHoa = hanghoa.IdHangHoa;
            hh.MoTa = hanghoa.MoTa;
            hh.SoLuong = hanghoa.SoLuong; ;
            hh.DonViTinh =  hanghoa.DonViTinh;
            hh.TenHangHoa = hanghoa.TenHangHoa;
            hh.IdDanhMuc = hanghoa.IdDanhMuc;

            hangHoaService.Add(hh);

            if(hangHoaService.getById(hh.IdHangHoa) != null)
            {
                TbChiTietHangUngHo ct = new TbChiTietHangUngHo();
                ct.IdHangHoa = hh.IdHangHoa;
                ct.IdDonDk = hanghoa.idDonDK;
                ct.SoLuong = hanghoa.SoLuong;

                chiTietHangUngService.save(ct);
            }

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


        [HttpGet]
        public IActionResult DetailsProduct(long id)
        {
            var data = hangHoaService.GetDetailsProduct(id);
            if (data == null)
            {
                return NotFound(); 
            }
            return View(data);
        }

    }
}
