using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAPM.Data;
using DAPM.Models;

namespace DAPM.Controllers
{
    public class TbDotLusController : Controller
    {
        private readonly Data.DbLuLutHoaVangContext _context;

        public TbDotLusController(Data.DbLuLutHoaVangContext context)
        {
            _context = context;
        }

        // GET: TbDotLus
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbDotLus.ToListAsync());
        }

        // GET: TbDotLus/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDotLu = await _context.TbDotLus
                .FirstOrDefaultAsync(m => m.IdDotLu == id);
            if (tbDotLu == null)
            {
                return NotFound();
            }

            return View(tbDotLu);
        }

        // GET: TbDotLus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbDotLus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDotLu,TenDotLu,NgayBatDau,NgayKetThuc")] TbDotLu tbDotLu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbDotLu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbDotLu);
        }

        
        // GET: TbDotLus/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDotLu = await _context.TbDotLus.FindAsync(id);
            if (tbDotLu == null)
            {
                return NotFound();
            }

            return View(tbDotLu); // Truyền dữ liệu của bản ghi cần chỉnh sửa vào view
        }

        // POST: TbDotLus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdDotLu,TenDotLu,NgayBatDau,NgayKetThuc")] TbDotLu tbDotLu)
        {
            if (id != tbDotLu.IdDotLu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbDotLu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbDotLuExists(tbDotLu.IdDotLu))
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
            return View(tbDotLu);
        }

        // GET: TbDotLus/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDotLu = await _context.TbDotLus
                .FirstOrDefaultAsync(m => m.IdDotLu == id);
            if (tbDotLu == null)
            {
                return NotFound();
            }

            return View(tbDotLu);
        }

        // POST: TbDotLus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tbDotLu = await _context.TbDotLus.FindAsync(id);
            if (tbDotLu != null)
            {
                _context.TbDotLus.Remove(tbDotLu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbDotLuExists(long id)
        {
            return _context.TbDotLus.Any(e => e.IdDotLu == id);
        }
    }
}
