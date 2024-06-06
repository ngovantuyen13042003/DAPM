using DAPM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace DAPM.Controllers
{
    public class DanhMucsController : Controller
    {
        private readonly Data.DbLuLutHoaVangContext _context;

        public DanhMucsController(Data.DbLuLutHoaVangContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstdanhmuc = _context.TbDanhMucs.AsNoTracking().OrderBy(x => x.IdDanhMuc);
            PagedList<TbDanhMuc> lst = new PagedList<TbDanhMuc>(lstdanhmuc, pageNumber, pageSize);
            return View(lst);
        }

        [Route("DanhMucs/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: DanhMucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("DanhMucs/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TbDanhMuc tbDanhMuc)
        {
            if (ModelState.IsValid)
            {
                _context.TbDanhMucs.Add(tbDanhMuc);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbDanhMuc);
        }

        // GET: TaiKhoans/Edit/5
        [Route("DanhMucs/Edit")]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDanhMuc = _context.TbDanhMucs.Find(Convert.ToInt64(id));
            if (tbDanhMuc == null)
            {
                return NotFound();
            }
            return View(tbDanhMuc);
        }

        [Route("DanhMucs/Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TbDanhMuc tbDanhMuc)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(tbDanhMuc).State = EntityState.Modified;
                _context.Update(tbDanhMuc);
                _context.SaveChanges();
                return RedirectToAction("Index", "DanhMucs");
            }
            return View(tbDanhMuc);
        }

        // GET: TaiKhoans/Delete/5
        [Route("DanhMucs/Delete")]
        [HttpGet]
        public IActionResult Delete(long? id)
        {
            TempData["Message"] = "";
            var tbHH = _context.TbHangHoas.Where(x => x.IdDanhMuc == id).ToList();
            if (tbHH.Count() > 0)
            {
                TempData["Message"] = "Không xóa được danh mục này!";
                return RedirectToAction("Index", "DanhMucs");
            }

            _context.Remove(_context.TbDanhMucs.Find(id));
            _context.SaveChanges();

            TempData["Message"] = "Xóa thành công!";
            return RedirectToAction("Index", "DanhMucs");
        }

        private bool TbDanhMucExists(long id)
        {
            return _context.TbDanhMucs.Any(e => e.IdDanhMuc == id);
        }
    }
}
