using DAPM.Data;
using DAPM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DAPM.Controllers
{
    public class CAPNHATTHIETHAIController : Controller
    {
        private readonly DbLuLutHoaVangContext _context;
        public CAPNHATTHIETHAIController(DbLuLutHoaVangContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var danhSachThietHai = await _context.TbChitietMucDoThietHais
                .Include(x => x.IdDotLuNavigation)
                .Include(x => x.IdMucDoNavigation)
                .Include(x => x.IdTaiKhoanNavigation)
                .Select(x => new
                {
                    IdMucDo = x.IdMucDo,
                    IdTaiKhoan = x.IdTaiKhoan,
                    IdDotLu = x.IdDotLu,
                    MucThietHai = x.IdMucDoNavigation.MucThietHai,
                    TenDangNhap = x.IdTaiKhoanNavigation.TenDangNhap,
                    TenDotLu = x.IdDotLuNavigation.TenDotLu,
                    MoTa = x.Mota
                })
                .ToListAsync();

            return View(danhSachThietHai);
        }
        public async Task<IActionResult> Edit(long idMucDo, long idTaiKhoan, long idDotLu)
        {
            var thietHai = await _context.TbChitietMucDoThietHais
                .FirstOrDefaultAsync(x => x.IdMucDo == idMucDo && x.IdTaiKhoan == idTaiKhoan && x.IdDotLu == idDotLu);

            if (thietHai == null)
            {
                return NotFound();
            }

            ViewBag.MucThietHai = new SelectList(_context.TbMucDoThietHais, "IdMucDo", "MucThietHai", thietHai.IdMucDo);
            ViewBag.TenDangNhap = new SelectList(_context.TbTaiKhoans, "IdTaiKhoan", "TenDangNhap", thietHai.IdTaiKhoan);
            ViewBag.TenDotLu = new SelectList(_context.TbDotLus, "IdDotLu", "TenDotLu", thietHai.IdDotLu);
            return View(thietHai);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long idMucDo, long idTaiKhoan, long idDotLu, TbChitietMucDoThietHai thietHaiToUpdate)
        {

            if (!ModelState.IsValid)
            {
                _context.Update(thietHaiToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(thietHaiToUpdate);
        }
        private bool ThietHaiExists(long idMucDo, long idTaiKhoan, long idDotLu)
        {
            return _context.TbChitietMucDoThietHais.Any(e => e.IdMucDo == idMucDo && e.IdTaiKhoan == idTaiKhoan && e.IdDotLu == idDotLu);
        }

        // POST: Xử lý xóa thực sự
        public async Task<IActionResult> Delete(long idMucDo, long idTaiKhoan, long idDotLu)
        {
            var thietHai = await _context.TbChitietMucDoThietHais
                .FirstOrDefaultAsync(x => x.IdMucDo == idMucDo && x.IdTaiKhoan == idTaiKhoan && x.IdDotLu == idDotLu);

            if (thietHai != null)
            {
                _context.TbChitietMucDoThietHais.Remove(thietHai);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        // GET: Hiển thị form thêm mới
        public IActionResult Create()
        {
            ViewBag.IdMucDo = new SelectList(_context.TbMucDoThietHais, "IdMucDo", "MucThietHai");
            ViewBag.IdTaiKhoan = new SelectList(_context.TbTaiKhoans, "IdTaiKhoan", "TenDangNhap");
            ViewBag.IdDotLu = new SelectList(_context.TbDotLus, "IdDotLu", "TenDotLu");
            return View();
        }

        // POST: Xử lý dữ liệu form thêm mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbChitietMucDoThietHai thietHai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thietHai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Trường hợp dữ liệu không hợp lệ, hiển thị lại form với thông tin đã nhập
            ViewBag.IdMucDo = new SelectList(_context.TbMucDoThietHais, "IdMucDo", "MucThietHai", thietHai.IdMucDo);
            ViewBag.IdTaiKhoan = new SelectList(_context.TbTaiKhoans, "IdTaiKhoan", "TenDangNhap", thietHai.IdTaiKhoan);
            ViewBag.IdDotLu = new SelectList(_context.TbDotLus, "IdDotLu", "TenDotLu", thietHai.IdDotLu);
            return View(thietHai);
        }
    }
}
