using System;
using System.Collections.Generic;

namespace DAPM.Models;

public partial class TbTaiKhoan
{
    public long IdTaiKhoan { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string Quyen { get; set; } = null!;

    public string? HoVaTen { get; set; }

    public string? Email { get; set; }

    public string? Sdt { get; set; }

    public string? DiaChi { get; set; }

    public string? TenPhuong { get; set; }

    public virtual ICollection<TbBaiDang> TbBaiDangs { get; set; } = new List<TbBaiDang>();

    public virtual ICollection<TbChiTietHangCuuTro> TbChiTietHangCuuTros { get; set; } = new List<TbChiTietHangCuuTro>();

    public virtual ICollection<TbChitietMucDoThietHai> TbChitietMucDoThietHais { get; set; } = new List<TbChitietMucDoThietHai>();

    public virtual ICollection<TbDonDangKyUngHo> TbDonDangKyUngHos { get; set; } = new List<TbDonDangKyUngHo>();

    public virtual ICollection<TbThongBao> TbThongBaos { get; set; } = new List<TbThongBao>();
}
