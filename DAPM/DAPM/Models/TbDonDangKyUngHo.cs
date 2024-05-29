using System;
using System.Collections.Generic;

namespace DAPM.Models;

public partial class TbDonDangKyUngHo
{
    public long IdDonDk { get; set; }

    public string? TenHangUngHo { get; set; }

    public DateTime? NgayDk { get; set; }

    public string? TrangThai { get; set; }

    public long? IdTaiKhoan { get; set; }

    public long? IdDotLu { get; set; }

    public virtual TbDotLu? IdDotLuNavigation { get; set; }

    public virtual TbTaiKhoan? IdTaiKhoanNavigation { get; set; }

    public virtual ICollection<TbChiTietHangUngHo> TbChiTietHangUngHos { get; set; } = new List<TbChiTietHangUngHo>();
}
