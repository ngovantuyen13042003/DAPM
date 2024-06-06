using DAPM.Data;
using DAPM.Models;
using DAPM.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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


        public DetailsProduct GetDetailsProduct(long idHangHoa)
        {
            var hh = mydb.TbHangHoas.Where(h => h.IdHangHoa == idHangHoa).FirstOrDefault();

            if (hh == null)
            {
                // Nếu không tìm thấy hàng hóa thì trả về null hoặc ném ngoại lệ.
                return null;
            }

            var details = new DetailsProduct
            {
                IdHangHoa = hh.IdHangHoa,
                TenHangHoa = hh.TenHangHoa,
                SoLuong = hh.SoLuong,
                DonViTinh = hh.DonViTinh,
                mota = hh.MoTa,
                IdDanhMuc = hh.IdDanhMuc
            };

            var dm = mydb.TbDanhMucs.FirstOrDefault(dm => dm.IdDanhMuc == hh.IdDanhMuc);
            if (dm != null)
            {
                details.TenDanhMuc = dm.TenDanhMuc;
            }

            var cthuh = mydb.TbChiTietHangUngHos.FirstOrDefault(ct => ct.IdHangHoa == hh.IdHangHoa);
            if (cthuh != null)
            {
                details.IdDonDKUngHo = cthuh.IdDonDk;

                var puh = mydb.TbDonDangKyUngHos.FirstOrDefault(p => p.IdDonDk == cthuh.IdDonDk);
                if (puh != null)
                {
                    var user = mydb.TbTaiKhoans.FirstOrDefault(tk => tk.IdTaiKhoan == puh.IdTaiKhoan);
                    if (user != null)
                    {
                        details.IdTaiKhoan = user.IdTaiKhoan;
                        details.username = user.TenDangNhap;
                        details.fullname = user.HoVaTen;
                    }

                    var dotlu = mydb.TbDotLus.FirstOrDefault(dl => dl.IdDotLu == puh.IdDotLu);
                    if (dotlu != null)
                    {
                        details.idDotLu = dotlu.IdDotLu;
                        details.tenDotLu = dotlu.TenDotLu;
                    }
                }
            }

            return details;
        }

        public DetailsProduct GetOneDetailsProduct(long idHangHoa)
        {
            var query = from hh in mydb.TbHangHoas
                        join dm in mydb.TbDanhMucs on hh.IdDanhMuc equals dm.IdDanhMuc
                        join cthuh in mydb.TbChiTietHangUngHos on hh.IdHangHoa equals cthuh.IdHangHoa into cthuhJoin
                        from cthuh in cthuhJoin.DefaultIfEmpty()
                        join puh in mydb.TbDonDangKyUngHos on cthuh.IdDonDk equals puh.IdDonDk into puhJoin
                        from puh in puhJoin.DefaultIfEmpty()
                        join user in mydb.TbTaiKhoans on puh.IdTaiKhoan equals user.IdTaiKhoan into userJoin
                        from user in userJoin.DefaultIfEmpty()
                        join dotlu in mydb.TbDotLus on puh.IdDotLu equals dotlu.IdDotLu into dotluJoin
                        from dotlu in dotluJoin.DefaultIfEmpty()
                        where hh.IdHangHoa == idHangHoa
                        select new DetailsProduct
                        {
                            IdHangHoa = hh.IdHangHoa,
                            TenHangHoa = hh.TenHangHoa,
                            SoLuong = hh.SoLuong,
                            DonViTinh = hh.DonViTinh,
                            mota = hh.MoTa,
                            IdDanhMuc = hh.IdDanhMuc,
                            TenDanhMuc = dm.TenDanhMuc,
                            IdDonDKUngHo = cthuh.IdDonDk,
                            IdTaiKhoan = user.IdTaiKhoan,
                            username = user.TenDangNhap,
                            fullname = user.HoVaTen,
                            idDotLu = dotlu.IdDotLu,
                            tenDotLu = dotlu.TenDotLu
                        };

            var details = query.FirstOrDefault();

            // Kiểm tra nếu không tìm thấy hàng hóa
            if (details == null)
            {
                return null;
            }

            // Kiểm tra và đặt giá trị mặc định nếu thông tin tài khoản hoặc đợt lũ không tồn tại
            if (details.IdDonDKUngHo == 0)
            {
                details.IdDonDKUngHo = 0;
                details.IdTaiKhoan = 0;
                details.username = "N/A";
                details.fullname = "N/A";
                details.idDotLu = 0;
                details.tenDotLu = "N/A";
            }

            return details;
        }



        public DetailsProduct GetSingleDetailsProduct(long idHangHoa)
        {
            var detailsProduct = (from hh in mydb.TbHangHoas
                                  where hh.IdHangHoa == idHangHoa
                                  join dm in mydb.TbDanhMucs on hh.IdDanhMuc equals dm.IdDanhMuc
                                  join cthuh in mydb.TbChiTietHangUngHos on hh.IdHangHoa equals cthuh.IdHangHoa
                                  join puh in mydb.TbDonDangKyUngHos on cthuh.IdDonDk equals puh.IdDonDk
                                  join user in mydb.TbTaiKhoans on puh.IdTaiKhoan equals user.IdTaiKhoan
                                  join dotlu in mydb.TbDotLus on puh.IdDotLu equals dotlu.IdDotLu
                                  select new DetailsProduct
                                  {
                                      IdHangHoa = hh.IdHangHoa,
                                      TenHangHoa = hh.TenHangHoa,
                                      mota = hh.MoTa,
                                      SoLuong = hh.SoLuong,
                                      DonViTinh = hh.DonViTinh,
                                      IdDanhMuc = hh.IdDanhMuc,
                                      TenDanhMuc = dm.TenDanhMuc,
                                      IdDonDKUngHo = cthuh.IdDonDk,
                                      IdTaiKhoan = user.IdTaiKhoan,
                                      username = user.TenDangNhap,
                                      fullname = user.HoVaTen,
                                      idDotLu = dotlu.IdDotLu,
                                      tenDotLu = dotlu.TenDotLu
                                  }).FirstOrDefault(); 

            return detailsProduct;
        }



    }
}
