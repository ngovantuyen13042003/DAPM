using DAPM.Data;
using DAPM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DAPM.Controllers
{
    public class DOTLUController : Controller
    {
        private DbLuLutHoaVangContext _context;
        public DOTLUController(DbLuLutHoaVangContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchTerm, int page = 1, int pageSize = 7)
        {
            IQueryable<TbDotLu> query = _context.TbDotLus.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(x => x.TenDotLu.Contains(searchTerm));
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var dotLus = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.SearchTerm = searchTerm;

            return View(dotLus);
        }
        public async Task<IActionResult> Edit(long id)
        {
            var dotLu = await _context.TbDotLus.FindAsync(id);
            if (dotLu == null)
            {
                return NotFound();
            }
            return View(dotLu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, TbDotLu dotLu)
        {
            if (id != dotLu.IdDotLu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(dotLu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dotLu);
        }
        public async Task<IActionResult> Delete(long id)
        {
            var dotLu = await _context.TbDotLus.FindAsync(id);
            if (dotLu == null)
            {
                return NotFound();
            }

            _context.TbDotLus.Remove(dotLu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbDotLu dotLu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dotLu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dotLu);
        }
    }
}
