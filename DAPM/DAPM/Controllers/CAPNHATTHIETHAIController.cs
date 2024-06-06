using DAPM.Data;
using DAPM.Models;
using DAPM.ViewModel;
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

        public async Task<IActionResult> Index(string searchTerm, int page = 1, int pageSize = 7)
        {
            var query = _context.TbChitietMucDoThietHais
                .Include(x => x.IdDotLuNavigation)
                .Include(x => x.IdMucDoNavigation)
                .Include(x => x.IdTaiKhoanNavigation)
                .Select(x => new
                {
                    IdMucDo = x.IdMucDo,
                    IdTaiKhoan = x.IdTaiKhoan,
                    IdDotLu = x.IdDotLu,
                    MucThietHai = x.IdMucDoNavigation.MucThietHai,
                    HoVaTen = x.IdTaiKhoanNavigation.HoVaTen,
                    TenDotLu = x.IdDotLuNavigation.TenDotLu,
                    MoTa = x.Mota
                });

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(x => x.HoVaTen.Contains(searchTerm) ||
                                          x.TenDotLu.Contains(searchTerm) ||
                                          x.MucThietHai.Contains(searchTerm));
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var danhSachThietHai = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.SearchTerm = searchTerm; // Lưu trữ searchTerm trong ViewBag

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

            var viewModel = new ThietHaiViewModel
            {
                OldIdMucDo = thietHai.IdMucDo,
                OldIdTaiKhoan = thietHai.IdTaiKhoan,
                OldIdDotLu = thietHai.IdDotLu,
                IdMucDo = thietHai.IdMucDo,
                IdTaiKhoan = thietHai.IdTaiKhoan,
                IdDotLu = thietHai.IdDotLu,
                Mota = thietHai.Mota
            };

            ViewBag.IdMucDo = new SelectList(_context.TbMucDoThietHais, "IdMucDo", "MucThietHai", thietHai.IdMucDo);
            ViewBag.IdTaiKhoan = new SelectList(_context.TbTaiKhoans, "IdTaiKhoan", "HoVaTen", thietHai.IdTaiKhoan);
            ViewBag.IdDotLu = new SelectList(_context.TbDotLus, "IdDotLu", "TenDotLu", thietHai.IdDotLu);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ThietHaiViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var thietHai = await _context.TbChitietMucDoThietHais
                    .FirstOrDefaultAsync(x => x.IdMucDo == viewModel.OldIdMucDo && x.IdTaiKhoan == viewModel.OldIdTaiKhoan && x.IdDotLu == viewModel.OldIdDotLu);

                if (thietHai == null)
                {
                    return NotFound();
                }

                // Xóa bản ghi cũ
                _context.TbChitietMucDoThietHais.Remove(thietHai);

                // Tạo bản ghi mới
                var newThietHai = new TbChitietMucDoThietHai
                {
                    IdMucDo = viewModel.IdMucDo,
                    IdTaiKhoan = viewModel.IdTaiKhoan,
                    IdDotLu = viewModel.IdDotLu,
                    Mota = viewModel.Mota
                };

                _context.Add(newThietHai);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThietHaiExists(viewModel.IdMucDo, viewModel.IdTaiKhoan, viewModel.IdDotLu))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.IdMucDo = new SelectList(_context.TbMucDoThietHais, "IdMucDo", "MucThietHai", viewModel.IdMucDo);
            ViewBag.IdTaiKhoan = new SelectList(_context.TbTaiKhoans, "IdTaiKhoan", "HoVaTen", viewModel.IdTaiKhoan);
            ViewBag.IdDotLu = new SelectList(_context.TbDotLus, "IdDotLu", "TenDotLu", viewModel.IdDotLu);

            return View(viewModel);
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
            ViewBag.IdTaiKhoan = new SelectList(_context.TbTaiKhoans, "IdTaiKhoan", "HoVaTen");
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
            ViewBag.IdTaiKhoan = new SelectList(_context.TbTaiKhoans, "IdTaiKhoan", "HoVaTen", thietHai.IdTaiKhoan);
            ViewBag.IdDotLu = new SelectList(_context.TbDotLus, "IdDotLu", "TenDotLu", thietHai.IdDotLu);
            return View(thietHai);
        }
    }
}
