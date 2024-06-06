using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAPM.Data;
using DAPM.Models;
using DAPM.Models.ViewModels;

namespace DAPM.Controllers
{
    public class TbChiTietHangCuuTroesController : Controller
    {
        private readonly Data.DbLuLutHoaVangContext _context;

        public TbChiTietHangCuuTroesController(Data.DbLuLutHoaVangContext context)
        {
            _context = context;
        }

        // GET: TbChiTietHangCuuTroes
        public async Task<IActionResult> Index()
        {
            var result = from cthct in _context.TbChiTietHangCuuTros
                         join hh in _context.TbHangHoas on cthct.IdHangHoa equals hh.IdHangHoa
                         join dct in _context.TbDotCuuTros on cthct.IdDotCuuTro equals dct.IdDotCuuTro
                         join dl in _context.TbDotLus on dct.IdDotLu equals dl.IdDotLu
                         join ctmtd in _context.TbChitietMucDoThietHais on cthct.IdTaiKhoan equals ctmtd.IdTaiKhoan
                         join mdt in _context.TbMucDoThietHais on ctmtd.IdMucDo equals mdt.IdMucDo
                         select new TbChiTietHangCuuTroViewModel
                         {
                             IdHangHoa = cthct.IdHangHoa,
                             IdTaiKhoan = cthct.IdTaiKhoan,
                             IdDotCuuTro = cthct.IdDotCuuTro,
                             SoLuong = cthct.SoLuong,
                             TenDotLu = dl.TenDotLu,
                             TenDotCuuTro = dct.TenDotCuuTro,
                             TenMucDoThietHai = mdt.MucThietHai,
                             TenHangHoa = hh.TenHangHoa,
                             TenTaiKhoan = cthct.IdTaiKhoanNavigation.HoVaTen
                         };

            var resultList = await result.ToListAsync();
            return View(resultList);
        }

        // GET: TbChiTietHangCuuTroes/Details/5
        public async Task<IActionResult> Details(long? idHangHoa, long? idTaiKhoan, long? idDotCuuTro)
        {
            if (idHangHoa == null || idTaiKhoan == null || idDotCuuTro == null)
            {
                return NotFound();
            }

            var tbChiTietHangCuuTro = await _context.TbChiTietHangCuuTros
                .Include(t => t.IdDotCuuTroNavigation)
                .Include(t => t.IdHangHoaNavigation)
                .Include(t => t.IdTaiKhoanNavigation)
                .FirstOrDefaultAsync(m => m.IdHangHoa == idHangHoa && m.IdTaiKhoan == idTaiKhoan && m.IdDotCuuTro == idDotCuuTro);
            if (tbChiTietHangCuuTro == null)
            {
                return NotFound();
            }

            var viewModel = new TbChiTietHangCuuTroViewModel
            {
                IdTaiKhoan = tbChiTietHangCuuTro.IdTaiKhoan,
                IdDotCuuTro = tbChiTietHangCuuTro.IdDotCuuTro,
                IdHangHoa = tbChiTietHangCuuTro.IdHangHoa,
                SoLuong = tbChiTietHangCuuTro.SoLuong,
                TenDotCuuTro = tbChiTietHangCuuTro.IdDotCuuTroNavigation.TenDotCuuTro,
                TenHangHoa = tbChiTietHangCuuTro.IdHangHoaNavigation.TenHangHoa,
                TenTaiKhoan = tbChiTietHangCuuTro.IdTaiKhoanNavigation.HoVaTen
            };

            return View(viewModel);
        }


        // GET: TbChiTietHangCuuTroes/Create
        public IActionResult Create()
        {
            ViewData["IdDotCuuTro"] = new SelectList(_context.TbDotCuuTros, "IdDotCuuTro", "TenDotCuuTro");
            ViewData["IdHangHoa"] = new SelectList(_context.TbHangHoas, "IdHangHoa", "TenHangHoa");
            ViewData["IdTaiKhoan"] = new SelectList(_context.TbTaiKhoans, "IdTaiKhoan", "HoVaTen");
            return View();
        }

        // POST: TbChiTietHangCuuTroes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHangHoa,IdTaiKhoan,IdDotCuuTro,SoLuong")] TbChiTietHangCuuTro tbChiTietHangCuuTro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbChiTietHangCuuTro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDotCuuTro"] = new SelectList(_context.TbDotCuuTros, "IdDotCuuTro", "TenDotCuuTro", tbChiTietHangCuuTro.IdDotCuuTro);
            ViewData["IdHangHoa"] = new SelectList(_context.TbHangHoas, "IdHangHoa", "TenHangHoa", tbChiTietHangCuuTro.IdHangHoa);
            ViewData["IdTaiKhoan"] = new SelectList(_context.TbTaiKhoans, "IdTaiKhoan", "HoVaTen", tbChiTietHangCuuTro.IdTaiKhoan);
            return View(tbChiTietHangCuuTro);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(long idTaiKhoan, long idDotCuuTro, long idHangHoa)
        {
            var chiTietHangCuuTro = await _context.TbChiTietHangCuuTros
                .Include(t => t.IdDotCuuTroNavigation)
                .Include(t => t.IdHangHoaNavigation)
                .Include(t => t.IdTaiKhoanNavigation)
                .FirstOrDefaultAsync(m => m.IdTaiKhoan == idTaiKhoan && m.IdDotCuuTro == idDotCuuTro && m.IdHangHoa == idHangHoa);

            if (chiTietHangCuuTro == null)
            {
                return NotFound();
            }

            var viewModel = new TbChiTietHangCuuTroViewModel
            {
                IdTaiKhoan = chiTietHangCuuTro.IdTaiKhoan,
                IdDotCuuTro = chiTietHangCuuTro.IdDotCuuTro,
                IdHangHoa = chiTietHangCuuTro.IdHangHoa,
                SoLuong = chiTietHangCuuTro.SoLuong,
                TenDotCuuTro = chiTietHangCuuTro.IdDotCuuTroNavigation.TenDotCuuTro,
                TenHangHoa = chiTietHangCuuTro.IdHangHoaNavigation.TenHangHoa,
                TenTaiKhoan = chiTietHangCuuTro.IdTaiKhoanNavigation.HoVaTen
            };

            // Đảm bảo số lượng hàng hóa cũng được truyền vào view
            ViewData["SoLuong"] = chiTietHangCuuTro.SoLuong;
            
           
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long idTaiKhoan, long idDotCuuTro, long idHangHoa, [Bind("IdTaiKhoan,IdDotCuuTro,IdHangHoa,SoLuong")] TbChiTietHangCuuTroViewModel viewModel)
        {
            if (idTaiKhoan != viewModel.IdTaiKhoan || idDotCuuTro != viewModel.IdDotCuuTro || idHangHoa != viewModel.IdHangHoa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var chiTietHangCuuTro = await _context.TbChiTietHangCuuTros
                        .FirstOrDefaultAsync(m => m.IdTaiKhoan == viewModel.IdTaiKhoan && m.IdDotCuuTro == viewModel.IdDotCuuTro && m.IdHangHoa == viewModel.IdHangHoa);

                    if (chiTietHangCuuTro == null)
                    {
                        return NotFound();
                    }

                    // Cập nhật số lượng hàng hóa
                    chiTietHangCuuTro.SoLuong = viewModel.SoLuong;

                    _context.Update(chiTietHangCuuTro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbChiTietHangCuuTroExists(viewModel.IdHangHoa, viewModel.IdTaiKhoan, viewModel.IdDotCuuTro))
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

            // Nếu ModelState không hợp lệ, hiển thị form chỉnh sửa với dữ liệu đã nhập
            return View(viewModel);
        }


        // GET: TbChiTietHangCuuTroes/Delete/5
        public async Task<IActionResult> Delete(long? idHangHoa, long? idTaiKhoan, long? idDotCuuTro)
        {
            if (idHangHoa == null || idTaiKhoan == null || idDotCuuTro == null)
            {
                return NotFound();
            }

            var tbChiTietHangCuuTro = await _context.TbChiTietHangCuuTros
                .Include(t => t.IdDotCuuTroNavigation)
                .Include(t => t.IdHangHoaNavigation)
                .Include(t => t.IdTaiKhoanNavigation)
                .FirstOrDefaultAsync(m => m.IdHangHoa == idHangHoa && m.IdTaiKhoan == idTaiKhoan && m.IdDotCuuTro == idDotCuuTro);
            if (tbChiTietHangCuuTro == null)
            {
                return NotFound();
            }

            return View(tbChiTietHangCuuTro);
        }

        // POST: TbChiTietHangCuuTroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? idHangHoa, long? idTaiKhoan, long? idDotCuuTro)
        {
            var tbChiTietHangCuuTro = await _context.TbChiTietHangCuuTros.FindAsync(idHangHoa, idTaiKhoan, idDotCuuTro);
            _context.TbChiTietHangCuuTros.Remove(tbChiTietHangCuuTro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbChiTietHangCuuTroExists(long idHangHoa, long idTaiKhoan, long idDotCuuTro)
        {
            return _context.TbChiTietHangCuuTros.Any(e => e.IdHangHoa == idHangHoa && e.IdTaiKhoan == idTaiKhoan && e.IdDotCuuTro == idDotCuuTro);
        }
    }
}
