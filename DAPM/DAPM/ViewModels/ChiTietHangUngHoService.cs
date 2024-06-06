using DAPM.Data;
using DAPM.Models;

namespace DAPM.ViewModels
{
    public class ChiTietHangUngHoService
    {
        private readonly DbLuLutHoaVangContext context;
        public ChiTietHangUngHoService(DbLuLutHoaVangContext context)
        {
            this.context = context;
        }

        public void save(TbChiTietHangUngHo  chiTietHangUngHo)
        {
             context.TbChiTietHangUngHos.Add(chiTietHangUngHo);
            context.SaveChanges();
        }
    }
}
