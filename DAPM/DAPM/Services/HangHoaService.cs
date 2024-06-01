using DAPM.Data;
using DAPM.Models;
using DAPM.ViewModels;

namespace DAPM.Services
{
    public class HangHoaService
    {
        private readonly DbLuLutHoaVangContext mydb;
        public HangHoaService(DbLuLutHoaVangContext Mydb) 
        { 
            mydb = Mydb;
        }
        public List<HangHoaViewModel> getAll()
        {
            var listhh = mydb.TbHangHoas.ToList().OrderBy(hh => hh.TenHangHoa);
            List<HangHoaViewModel> listfullhh = new List<HangHoaViewModel>();
            HangHoaViewModel hanghoa;
            foreach (var item in listhh)
            {
                hanghoa = new HangHoaViewModel();
                hanghoa.IdHangHoa = item.IdHangHoa;
                hanghoa.TenHangHoa = item.TenHangHoa;
                hanghoa.MoTa = item.MoTa;
                hanghoa.SoLuong = item.SoLuong;
                hanghoa.DonViTinh = item.DonViTinh;
                hanghoa.IdDanhMuc = item.IdDanhMuc;
                var dm = mydb.TbDanhMucs.FirstOrDefault(dm => dm.IdDanhMuc == item.IdDanhMuc);
                hanghoa.TenDanhMuc = dm.TenDanhMuc;
                listfullhh.Add(hanghoa);
            }
            return listfullhh;
        }

        public TbHangHoa getById(long id)
        {
            var hangHoa = mydb.TbHangHoas.Where(h => h.IdHangHoa == id).FirstOrDefault();
            return hangHoa;
        }

        public void Add(TbHangHoa hh)
        {
            mydb.TbHangHoas.Add(hh);
            mydb.SaveChanges();
        }

        public void Edit(TbHangHoa hh)
        {
            TbHangHoa hanghoa = mydb.TbHangHoas.Find(hh.IdHangHoa);
            hanghoa.TenHangHoa = hh.TenHangHoa;
            hanghoa.SoLuong = hh.SoLuong;
            hanghoa.MoTa =  hh.MoTa;
            hanghoa.DonViTinh = hh.DonViTinh;
            hanghoa.IdDanhMuc = hh.IdDanhMuc;
            mydb.SaveChanges();
        }


        public void Delete(TbHangHoa hh)
        {
            mydb.TbHangHoas.Remove(hh);
            mydb.SaveChanges();
        }
    }
}
