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

        public List<TbTaiKhoan> filterTaiKhoan(long? mucThietHai, long? dotlu, long dc)
        {
            var dct = context.TbDotCuuTros.FirstOrDefault(dct => dct.IdDotCuuTro == dc);
            if (dct != null)
            {
                var result = (from ct in context.TbChitietMucDoThietHais
                              join md in context.TbMucDoThietHais on ct.IdMucDo equals md.IdMucDo
                              join tk in context.TbTaiKhoans on ct.IdTaiKhoan equals tk.IdTaiKhoan
                              join dl in context.TbDotLus on ct.IdDotLu equals dotlu
                              where md.IdMucDo == mucThietHai && dct.IdDotLu == dl.IdDotLu && ct.IdDotLu == dotlu
                              select tk
                         ).Distinct().ToList();
                return result;
            }else
            {
                var result = (from ct in context.TbChitietMucDoThietHais
                              join md in context.TbMucDoThietHais on ct.IdMucDo equals md.IdMucDo
                              join tk in context.TbTaiKhoans on ct.IdTaiKhoan equals tk.IdTaiKhoan
                              join dl in context.TbDotLus on ct.IdDotLu equals dotlu
                              where md.IdMucDo == mucThietHai && ct.IdDotLu == dotlu
                              select tk
                         ).Distinct().ToList();
                return result;
            }
            
        }



    }
}
 