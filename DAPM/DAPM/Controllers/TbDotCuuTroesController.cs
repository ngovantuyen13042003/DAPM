using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAPM.Data;
using DAPM.Models;
using DAPM.ViewModels;
using X.PagedList;

namespace DAPM.Controllers
{
    public class TbDotCuuTroesController : Controller
    {
        private readonly Data.DbLuLutHoaVangContext _context;

        public TbDotCuuTroesController(Data.DbLuLutHoaVangContext context)
        {
            _context = context;
        }

        // GET: TbDotCuuTroes
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var lisCuuTro =  _context.TbDotCuuTros.Include(t => t.IdDotLuNavigation);

            PagedList<TbDotCuuTro> pagination = new PagedList<TbDotCuuTro>(lisCuuTro, pageNumber, pageSize);

            return View(pagination);
        }

        // GET: TbDotCuuTroes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDotCuuTro = await _context.TbDotCuuTros
                .Include(t => t.IdDotLuNavigation)
                .FirstOrDefaultAsync(m => m.IdDotCuuTro == id);
            if (tbDotCuuTro == null)
            {
                return NotFound();
            }

            return View(tbDotCuuTro);
        }

        // GET: TbDotCuuTroes/Create
        public IActionResult Create()
        {
            ViewBag.IdDotLu = new SelectList(_context.TbDotLus, "IdDotLu", "TenDotLu");
            return View();
        }

        // POST: TbDotCuuTroes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDotCuuTro,TenDotCuuTro,NgayBatDau,NgayKetThuc,IdDotLu")] TbDotCuuTro tbDotCuuTro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbDotCuuTro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.IdDotLu = new SelectList(_context.TbDotLus, "IdDotLu", "TenDotLu", tbDotCuuTro.IdDotLu);
            return View(tbDotCuuTro);
        }

        // GET: TbDotCuuTroes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDotCuuTro = await _context.TbDotCuuTros.FindAsync(id);
            if (tbDotCuuTro == null)
            {
                return NotFound();
            }
            ViewBag.IdDotLu = new SelectList(_context.TbDotLus, "IdDotLu", "TenDotLu", tbDotCuuTro.IdDotLu);
            return View(tbDotCuuTro);
        }

        // POST: TbDotCuuTroes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdDotCuuTro,TenDotCuuTro,NgayBatDau,NgayKetThuc,IdDotLu")] TbDotCuuTro tbDotCuuTro)
        {
            if (id != tbDotCuuTro.IdDotCuuTro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbDotCuuTro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbDotCuuTroExists(tbDotCuuTro.IdDotCuuTro))
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
            ViewBag.IdDotLu = new SelectList(_context.TbDotLus, "IdDotLu", "TenDotLu", tbDotCuuTro.IdDotLu);
            return View(tbDotCuuTro);
        }

        // GET: TbDotCuuTroes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDotCuuTro = await _context.TbDotCuuTros
                .Include(t => t.IdDotLuNavigation)
                .FirstOrDefaultAsync(m => m.IdDotCuuTro == id);
            if (tbDotCuuTro == null)
            {
                return NotFound();
            }

            return View(tbDotCuuTro);
        }

        // POST: TbDotCuuTroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tbDotCuuTro = await _context.TbDotCuuTros.FindAsync(id);
            if (tbDotCuuTro != null)
            {
                _context.TbDotCuuTros.Remove(tbDotCuuTro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbDotCuuTroExists(long id)
        {
            return _context.TbDotCuuTros.Any(e => e.IdDotCuuTro == id);
        }
    }
}
