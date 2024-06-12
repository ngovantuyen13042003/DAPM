namespace DAPM.Models.ViewModels
{
    public class TbChiTietHangCuuTroViewModel
    {
        public long IdHangHoa { get; set; }
        public long IdTaiKhoan { get; set; }
        public long IdDotCuuTro { get; set; }
        public long IdMucDoThietHai { get; set; } 
        public int SoLuong { get; set; }
        public string TenDotLu { get; set; }
        public string TenDotCuuTro { get; set; }
        public string TenMucDoThietHai { get; set; }
        public string TenHangHoa { get; set; }
        public string TenTaiKhoan { get; set; }
    }
}
