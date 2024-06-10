using DAPM.Data;
using DAPM.Models;

namespace DAPM.Services
{
    public class CuuTroService
    {

        private readonly DbLuLutHoaVangContext context;

        public CuuTroService(DbLuLutHoaVangContext context)
        {
            this.context = context;
        }



        public List<TbDotCuuTro> filterDotCuuTro(long? dl)
        {
            var result = context.TbDotCuuTros.Where(dct => dct.IdDotLu == dl).ToList();
            return result;
        }

        public List<TbMucDoThietHai> getAllMucThietHai()
        {
            var result = context.TbMucDoThietHais.ToList();
            return result;
        }

        public List<TbDotLu> getAllMucDotLu()
        {
            var result = context.TbDotLus.ToList();
            return result;
        }

        public List<TbDanhMuc> getAllDanhMuc()
        {
            var result = context.TbDanhMucs.ToList();
            return result;
        }


        public List<TbHangHoa> filterHangHoa(long? dm)
        {
            var result = context.TbHangHoas.Where(hh => hh.IdDanhMuc == dm).ToList();
            return result;
        }

        public List<TbTaiKhoan> filterTaiKhoan(long? mucThietHai)
        {
            var result = (from md in context.TbMucDoThietHais
                          join ct in context.TbChitietMucDoThietHais on md.IdMucDo equals ct.IdMucDo
                          join tk in context.TbTaiKhoans on ct.IdTaiKhoan equals tk.IdTaiKhoan
                          where md.IdMucDo == mucThietHai
                           select tk 
                         ).Distinct().ToList();
            return result;
        }



    }
}
 