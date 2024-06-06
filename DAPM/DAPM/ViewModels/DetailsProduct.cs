namespace DAPM.ViewModels
{
    public class DetailsProduct
    {
        public long IdHangHoa { get; set; }

        public string TenHangHoa { get; set; } = null!;

        public int SoLuong { get; set; }
        public string mota {  get; set; }

        public string DonViTinh { get; set; } = null!;

        public long? IdDanhMuc { get; set; }
        public string TenDanhMuc { get; set; }
        public long IdDonDKUngHo { get; set; }
        public long IdTaiKhoan { get; set; }
        public string username { get; set; }
        public string? fullname { get; set; }

        public long idDotLu {  get; set; }
        public string tenDotLu { get; set; }
    }
}
