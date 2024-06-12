using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAPM.Data;
using DAPM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DAPM.Controllers
{
    public class BAIDANGController : Controller
    {
        private readonly DbLuLutHoaVangContext _context;

        public BAIDANGController(DbLuLutHoaVangContext context)
        {
            _context = context;
        }

        // GET: BAIDANG
        public async Task<IActionResult> Index(string searchTerm, int page = 1, int pageSize = 5)
        {
            IQueryable<TbBaiDang> query = _context.TbBaiDangs
                .Include(t => t.IdDotLuNavigation)
                .Include(t => t.IdTaiKhoanNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(x => x.TieuDe.Contains(searchTerm) || x.NoiDung.Contains(searchTerm));
            }
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var baidangs = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.SearchTerm = searchTerm;

            return View(baidangs);
        }

        // GET: BAIDANG/Create
        public IActionResult Create()
        {
            ViewData["IdDotLu"] = new SelectList(_context.TbDotLus, "IdDotLu", "TenDotLu");
            ViewData["IdTaiKhoan"] = new SelectList(_context.TbTaiKhoans.Where(t => t.Quyen == "admin"), "IdTaiKhoan", "HoVaTen");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBaiDang,TieuDe,NoiDung,NgayDang,IdDotLu,IdTaiKhoan")] TbBaiDang tbBaiDang, List<IFormFile> Images)
        {
            if (ModelState.IsValid)
            {
                // Đặt giá trị mặc định cho NgayDang nếu chưa được đặt
                if (tbBaiDang.NgayDang == null)
                {
                    tbBaiDang.NgayDang = DateTime.Now;
                }

                _context.Add(tbBaiDang);
                await _context.SaveChangesAsync();

                foreach (var file in Images.Take(3))
                {
                    if (file.Length > 0)
                    {
                        var filePath = Path.Combine("wwwroot/dotlu", file.FileName);// tạo đường dẫn
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var hinhAnh = new TbHinhAnh
                        {
                            UrlHinhAnh = "/dotlu/" + file.FileName,
                            IdBaiDang = tbBaiDang.IdBaiDang
                        };
                        _context.TbHinhAnhs.Add(hinhAnh);                        
                    }
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDotLu"] = new SelectList(_context.TbDotLus, "IdDotLu", "TenDotLu", tbBaiDang.IdDotLu);
            ViewData["IdTaiKhoan"] = new SelectList(_context.TbTaiKhoans, "IdTaiKhoan", "HoVaTen", tbBaiDang.IdTaiKhoan);
            return View(tbBaiDang);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbBaiDang = await _context.TbBaiDangs
                .Include(b => b.IdDotLuNavigation)
                .Include(b => b.IdTaiKhoanNavigation)
                .Include(b => b.TbHinhAnhs) // Bao gồm các hình ảnh liên quan
                .FirstOrDefaultAsync(m => m.IdBaiDang == id);

            if (tbBaiDang == null)
            {
                return NotFound();
            }

            return View(tbBaiDang);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbBaiDang = await _context.TbBaiDangs
                .Include(b => b.IdDotLuNavigation)
                .Include(b => b.IdTaiKhoanNavigation)
                .Include(b => b.TbHinhAnhs) // Bao gồm các hình ảnh liên quan
                .FirstOrDefaultAsync(m => m.IdBaiDang == id);

            if (tbBaiDang == null)
            {
                return NotFound();
            }

            ViewData["IdDotLu"] = new SelectList(_context.TbDotLus, "IdDotLu", "TenDotLu", tbBaiDang.IdDotLu);
            ViewData["IdTaiKhoan"] = new SelectList(_context.TbTaiKhoans.Where(t => t.Quyen == "Admin"), "IdTaiKhoan", "HoVaTen", tbBaiDang.IdTaiKhoan);
            return View(tbBaiDang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdBaiDang,TieuDe,NoiDung,NgayDang,IdDotLu,IdTaiKhoan")] TbBaiDang tbBaiDang, List<IFormFile> Images, string[] ExistingImages, string[] DeletedImages)
        {
            if (id != tbBaiDang.IdBaiDang)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbBaiDang);
                    await _context.SaveChangesAsync();

                    // Xóa hình ảnh bị xóa bởi người dùng
                    if (DeletedImages != null)
                    {
                        var deletedImageIds = DeletedImages.Select(long.Parse).ToList();
                        var imagesToDelete = _context.TbHinhAnhs.Where(h => deletedImageIds.Contains(h.IdHinhAnh)).ToList();
                        foreach (var image in imagesToDelete)
                        {
                            var filePath = Path.Combine("wwwroot", "dotlu", image.UrlHinhAnh);
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }
                            _context.TbHinhAnhs.Remove(image);
                        }
                        await _context.SaveChangesAsync();
                    }

                    // Thêm hình ảnh mới
                    foreach (var file in Images.Take(3 - ExistingImages.Length))
                    {
                        if (file.Length > 0)
                        {
                            //var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                            var filePath = Path.Combine("wwwroot/dotlu", file.FileName);// tạo đường dẫn
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            var hinhAnh = new TbHinhAnh
                            {
                                UrlHinhAnh = "/dotlu/" + file.FileName,
                                IdBaiDang = tbBaiDang.IdBaiDang
                            };
                            _context.TbHinhAnhs.Add(hinhAnh);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbBaiDangExists(tbBaiDang.IdBaiDang))
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
            ViewData["IdDotLu"] = new SelectList(_context.TbDotLus, "IdDotLu", "TenDotLu", tbBaiDang.IdDotLu);
            ViewData["IdTaiKhoan"] = new SelectList(_context.TbTaiKhoans, "IdTaiKhoan", "HoVaTen", tbBaiDang.IdTaiKhoan);
            return View(tbBaiDang);
        }




        // POST: BAIDANG/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long Idbaidang)
        {
            var baidang = await _context.TbBaiDangs.FindAsync(Idbaidang);
            if (baidang == null)
            {
                return NotFound();
            }

            _context.TbBaiDangs.Remove(baidang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool TbBaiDangExists(long id)
        {
            return _context.TbBaiDangs.Any(e => e.IdBaiDang == id);
        }
    }
}
