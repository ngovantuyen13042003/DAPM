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
        private readonly HangHoaService hangHoaService;

        public TbChiTietHangCuuTroesController(Data.DbLuLutHoaVangContext context, CuuTroService cuuTroService, HangHoaService hangHoaService)
        {
            _context = context;
            this.cuuTroService = cuuTroService;
            this.hangHoaService = hangHoaService;
        }

        // GET: TbChiTietHangCuuTroes

        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var result = _context.TbChiTietHangCuuTros
                .Select(cthct => new TbChiTietHangCuuTroViewModel
                {
                    IdHangHoa = cthct.IdHangHoa,
                    IdTaiKhoan = cthct.IdTaiKhoan,
                    IdDotCuuTro = cthct.IdDotCuuTro,
                    SoLuong = cthct.SoLuong,
                    TenDotLu = cthct.IdDotCuuTroNavigation.IdDotLuNavigation.TenDotLu,
                    TenDotCuuTro = cthct.IdDotCuuTroNavigation.TenDotCuuTro,
                    TenMucDoThietHai = cthct.IdTaiKhoanNavigation.TbChitietMucDoThietHais
                                        .Where(ctmtd => ctmtd.IdDotLu == cthct.IdDotCuuTroNavigation.IdDotLu)
                                        .Select(ctmtd => ctmtd.IdMucDoNavigation.MucThietHai)
                                        .FirstOrDefault(),
                    TenHangHoa = cthct.IdHangHoaNavigation.TenHangHoa,
                    TenTaiKhoan = cthct.IdTaiKhoanNavigation.HoVaTen
                }).ToList();


            PagedList<TbChiTietHangCuuTroViewModel> pagination = new PagedList<TbChiTietHangCuuTroViewModel>(result, pageNumber, pageSize);

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



        [HttpGet]
        public IActionResult Create(long? dl_filter, long? mth_filter, long? dm_filter, long ct)
        {
            var dl = _context.TbDotLus.ToList();
            var dm = cuuTroService.getAllDanhMuc();
            ViewBag.ct = ct;
            var mth = cuuTroService.getAllMucThietHai();

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
                tk = cuuTroService.filterTaiKhoan(mth_filter, dl_filter, ct);
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




        // POST: TbChiTietHangCuuTroes/Create
        [HttpPost]
        public IActionResult Create(ChiTietCuuTroModelView chiTietCuuTroModelView)
        {
            var product = _context.TbHangHoas.FirstOrDefault(hh => hh.IdHangHoa == chiTietCuuTroModelView.HangHoaId);

            var users = cuuTroService.filterTaiKhoan(chiTietCuuTroModelView.MucThietHaiId, chiTietCuuTroModelView.DotLuId, chiTietCuuTroModelView.DotCuuTroId);
            TempData["ErrorQuantity"] = "";

            if ((product?.SoLuong) < (users.Count * chiTietCuuTroModelView.SoLuong))
            {
                TempData["ErrorQuantity"] = "Số lượng nhập vượt quá số lượng hàng hóa còn lại!";
                return RedirectToAction("Create", "TbChiTietHangCuuTroes");
            }
            else
            {

                product.SoLuong -= users.Count * chiTietCuuTroModelView.SoLuong;
                hangHoaService.Edit(product);

                List<TbChiTietHangCuuTro> detail = new List<TbChiTietHangCuuTro>();

                foreach (var user in users)
                {
                    var chiTietHangCuuTro = new TbChiTietHangCuuTro
                    {
                        IdDotCuuTro = chiTietCuuTroModelView.DotCuuTroId,
                        IdHangHoa = chiTietCuuTroModelView.HangHoaId,
                        SoLuong = chiTietCuuTroModelView.SoLuong,
                        IdTaiKhoan = user.IdTaiKhoan 
                    };

                    detail.Add(chiTietHangCuuTro);
                }

                _context.AddRange(detail);
                _context.SaveChanges();
            }
            TempData["ErrorQuantity"] = "Thêm hàng cứu trợ thành công!";
            return RedirectToAction("Create", "TbChiTietHangCuuTroes");
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
