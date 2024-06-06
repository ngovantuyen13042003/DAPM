using DAPM.Data;
using DAPM.Models;
using DAPM.ViewModels;

namespace DAPM.Services
{
    public class DonDKService
    {
        private readonly DbLuLutHoaVangContext context;

        public DonDKService(DbLuLutHoaVangContext context)
        {
            this.context = context;
        }


        public List<DonDangKyViewModel>   getAll()
        {
            var data = context.TbDonDangKyUngHos.ToList();

            List<DonDangKyViewModel> list = new List<DonDangKyViewModel>();
            DonDangKyViewModel donDangKyView;
            foreach(var item in data)
            {
                donDangKyView = new DonDangKyViewModel();
                donDangKyView.IdDonDk = item.IdDonDk;
                donDangKyView.NgayDk = item.NgayDk;
                donDangKyView.IdDotLu = item.IdDotLu;
                donDangKyView.TenHangUngHo = item.TenHangUngHo;

                var tk = context.TbTaiKhoans.FirstOrDefault(tk => tk.IdTaiKhoan == item.IdTaiKhoan);

                donDangKyView.username = tk.TenDangNhap;
                donDangKyView.fullname = tk.HoVaTen;

                list.Add(donDangKyView);
            }

            return list;
        }
    }
}
