using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAPM.Data;
using DAPM.Models;
using X.PagedList;

namespace DAPM.Controllers
{
    public class TaiKhoansController : Controller
    {
        private readonly Data.DbLuLutHoaVangContext _context;

        public TaiKhoansController(Data.DbLuLutHoaVangContext context)
        {
            _context = context;
        }

        // GET: TaiKhoans
        
        public IActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lsttaikhoan = _context.TbTaiKhoans.AsNoTracking().OrderBy(x => x.IdTaiKhoan);
            PagedList<TbTaiKhoan> lst = new PagedList<TbTaiKhoan>(lsttaikhoan, pageNumber, pageSize);
            return View(lst);
        }

        // GET: TaiKhoans/Create
        [Route("TaiKhoans/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaiKhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("TaiKhoans/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TbTaiKhoan tbTaiKhoan)
        {
            if (ModelState.IsValid)
            {
                _context.TbTaiKhoans.Add(tbTaiKhoan);
                _context.SaveChanges();
                return RedirectToAction("Index", "TaiKhoans");
            }
            return View(tbTaiKhoan);
        }

        // GET: TaiKhoans/Edit/5
        [Route("TaiKhoans/Edit")]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTaiKhoan = _context.TbTaiKhoans.Find(Convert.ToInt64(id));
            if (tbTaiKhoan == null)
            {
                return NotFound();
            }
            return View(tbTaiKhoan);
        }

        // POST: TaiKhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("TaiKhoans/Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TbTaiKhoan tbTaiKhoan)
        { 
            if (ModelState.IsValid)
            {
                _context.Entry(tbTaiKhoan).State = EntityState.Modified;
                _context.Update(tbTaiKhoan);
                _context.SaveChanges();
                return RedirectToAction("Index", "TaiKhoans");
            }
            return View(tbTaiKhoan);
        }

        // GET: TaiKhoans/Delete/5
        [Route("TaiKhoans/Delete")]
        [HttpGet]
        public IActionResult Delete(long? id)
        {
            TempData["Message"] = "";
            var tbTB = _context.TbThongBaos.Where(x=>x.IdTaiKhoan==id).ToList();
            var tbDDK = _context.TbDonDangKyUngHos.Where(x => x.IdTaiKhoan == id).ToList();
            var tbCTMTH = _context.TbChitietMucDoThietHais.Where(x => x.IdTaiKhoan == id).ToList();
            var tbBD = _context.TbBaiDangs.Where(x => x.IdTaiKhoan == id).ToList();
            var tbCTCT = _context.TbChiTietHangCuuTros.Where(x => x.IdTaiKhoan == id).ToList();
            if (tbBD.Count() > 0 || tbCTCT.Count() > 0 || tbCTMTH.Count() >0 || tbDDK.Count()>0 || tbTB.Count()>0)
            {
                TempData["Message"] = "Không xóa được tài khoản này!";
                return RedirectToAction("Index", "TaiKhoans");
            }

            _context.Remove(_context.TbTaiKhoans.Find(id));
            _context.SaveChanges();

            TempData["Message"] = "Xóa thành công!";
            return RedirectToAction("Index", "TaiKhoans");
        }

        private bool TbTaiKhoanExists(long id)
        {
            return _context.TbTaiKhoans.Any(e => e.IdTaiKhoan == id);
        }
    }
}
