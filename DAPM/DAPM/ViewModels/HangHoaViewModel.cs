namespace DAPM.ViewModels
{
    public class HangHoaViewModel
    {
        public long IdHangHoa { get; set; }
        public string TenHangHoa { get; set; } = null!;
        public int SoLuong { get; set; }
        public string? MoTa { get; set; }
        public string DonViTinh { get; set; } = null!;  
        public long? IdDanhMuc { get; set; }
        public string TenDanhMuc { get; set; }
        public long idDonDK { get; set; }
        public string NguoiUngHo {  get; set; }
    }
}
