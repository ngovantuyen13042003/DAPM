using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAPM.Data;
using DAPM.Models;

namespace DAPM.Controllers
{
    public class DanhMucThietHaiController : Controller
    {
        private readonly DbLuLutHoaVangContext _context;

        public DanhMucThietHaiController(DbLuLutHoaVangContext context)
        {
            _context = context;
        }

        // GET: DanhMucThietHai
        public async Task<IActionResult> Index(string searchTerm)
        {
            IQueryable<TbMucDoThietHai> query = _context.TbMucDoThietHais;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(x => x.MucThietHai.Contains(searchTerm));
            }

            var mucThietHais = await query.ToListAsync();

            return View(mucThietHais);
        }

        // GET: DanhMucThietHai/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DanhMucThietHai/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMucDo,MucThietHai,MoTa")] TbMucDoThietHai tbMucDoThietHai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbMucDoThietHai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbMucDoThietHai);
        }

        // GET: DanhMucThietHai/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbMucDoThietHai = await _context.TbMucDoThietHais.FindAsync(id);
            if (tbMucDoThietHai == null)
            {
                return NotFound();
            }
            return View(tbMucDoThietHai);
        }

        // POST: DanhMucThietHai/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdMucDo,MucThietHai,MoTa")] TbMucDoThietHai tbMucDoThietHai)
        {
            if (id != tbMucDoThietHai.IdMucDo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbMucDoThietHai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbMucDoThietHaiExists(tbMucDoThietHai.IdMucDo))
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
            return View(tbMucDoThietHai);
        }

        public async Task<IActionResult> Delete(long id)
        {
            var tbMucDoThietHai = await _context.TbMucDoThietHais.FindAsync(id);
            if (tbMucDoThietHai == null)
            {
                return NotFound();
            }

            _context.TbMucDoThietHais.Remove(tbMucDoThietHai);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbMucDoThietHaiExists(long id)
        {
            return _context.TbMucDoThietHais.Any(e => e.IdMucDo == id);
        }
    }
}