using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAPM.Data;
using DAPM.Models;
using DAPM.Models.ViewModels;
using X.PagedList;
using DAPM.Services;
using DAPM.ViewModels;

namespace DAPM.Controllers
{
    public class TbChiTietHangCuuTroesController : Controller
    {
        private readonly Data.DbLuLutHoaVangContext _context;
        private readonly CuuTroService cuuTroService;

        public TbChiTietHangCuuTroesController(Data.DbLuLutHoaVangContext context, CuuTroService cuuTroService)
        {
            _context = context;
            this.cuuTroService = cuuTroService;
        }

        // GET: TbChiTietHangCuuTroes
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;


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
            PagedList<TbChiTietHangCuuTroViewModel> pagination = new PagedList<TbChiTietHangCuuTroViewModel>(resultList, pageNumber, pageSize);


            return View(pagination);
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
        public IActionResult Create(long? dl_filter, long? mth_filter, long? dm_filter, long? ct,  long? hang)
        {
            var dl = _context.TbDotLus.ToList();
            var dm = cuuTroService.getAllDanhMuc();
            ViewBag.ct = ct;
            var mth = cuuTroService.getAllMucThietHai();
            ViewBag.hh = hang;

            List<TbDotCuuTro> dct = null;
            List<TbTaiKhoan> tk = null;
            List<TbHangHoa> hh = null;

            if (dl_filter != null)
            {
                dct = cuuTroService.filterDotCuuTro(dl_filter);
                ViewBag.dl_filter = dl_filter;
            }

            if (mth_filter != null)
            {
                tk = cuuTroService.filterTaiKhoan(mth_filter);
                ViewBag.mth_filter = mth_filter;
            }

            if (dm_filter != null)
            {
                hh = cuuTroService.filterHangHoa(dm_filter);
                ViewBag.dm_filter = dm_filter;
            }

            var data = new Tuple<List<TbDotLu>, List<TbDanhMuc>, List<TbMucDoThietHai>, List<TbDotCuuTro>, List<TbTaiKhoan>, List<TbHangHoa>>(dl, dm, mth, dct, tk, hh);

            return View(data);
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
