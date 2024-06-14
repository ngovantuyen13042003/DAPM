using DAPM.Data;
using DAPM.Models;
using DAPM.ViewModels;
using System.Collections.Generic;


namespace DAPM.Services
{
    public class UngHoService
    {
        private readonly DbLuLutHoaVangContext _context;

        public UngHoService(DbLuLutHoaVangContext context)
        {
            _context = context;
        }

        public List<DonDangKyViewModel> getAll()
        {
            var list = (from don in _context.TbDonDangKyUngHos
                        join tk in _context.TbTaiKhoans on don.IdTaiKhoan equals tk.IdTaiKhoan
                        join dl in _context.TbDotLus on don.IdDotLu equals dl.IdDotLu
                        select new DonDangKyViewModel
                        {
                            IdDonDk = don.IdDonDk,
                            TenHangUngHo = don.TenHangUngHo,
                            NgayDk = don.NgayDk,
                            TrangThai = don.TrangThai,
                            IdTaiKhoan = don.IdTaiKhoan,
                            username = tk.TenDangNhap,
                            fullname = tk.HoVaTen,
                            IdDotLu = don.IdDotLu,
                            tenDotLu = dl.TenDotLu
                        }).OrderByDescending(t => t.NgayDk).ToList();
            return list;
        }

        public void ApproveUngHo(long? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "ID cannot be null");
            }

            var ungho = _context.TbDonDangKyUngHos.FirstOrDefault(uh => uh.IdDonDk == id);

            if (ungho == null)
            {
                throw new InvalidOperationException("UngHo not found");
            }
            if(ungho.TrangThai == "Đã duyệt" || ungho.TrangThai == "Đã trả")
            {
                ungho.TrangThai = "Chưa duyệt";
            }else
            {
                ungho.TrangThai = "Đã duyệt";
            }


            try
            {
                _context.Update(ungho);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error updating UngHo", ex);
            }
        }

        public List<DonDangKyViewModel> filter(string filter)
        {
            List<DonDangKyViewModel> list = getAll();

            List<DonDangKyViewModel> filteredList = new List<DonDangKyViewModel>();

            if(filter.Equals("all"))
            {
                return list;
            }

            foreach (var item in list)
            {
                if(item.TrangThai == "all")
                {
                    return list;
                }
                if (item.TrangThai == filter)
                {
                    filteredList.Add(item);
                }
            }

            return filteredList;
        }

        public void Add(TbDonDangKyUngHo donDangKyUngHo)
        {
            
                donDangKyUngHo.NgayDk = DateTime.Now;
                donDangKyUngHo.IdTaiKhoan = 1;
                donDangKyUngHo.TrangThai = "Chưa duyệt";
                _context.Add(donDangKyUngHo);
                _context.SaveChanges();
        }


    }
}
