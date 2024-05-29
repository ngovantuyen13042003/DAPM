using System;
using System.Collections.Generic;

namespace DAPM.Models;

public partial class TbHangHoa
{
    public long IdHangHoa { get; set; }

    public string TenHangHoa { get; set; } = null!;

    public int SoLuong { get; set; }

    public string? MoTa { get; set; }

    public string DonViTinh { get; set; } = null!;

    public long? IdDanhMuc { get; set; }

    public virtual TbDanhMuc? IdDanhMucNavigation { get; set; }

    public virtual ICollection<TbChiTietHangCuuTro> TbChiTietHangCuuTros { get; set; } = new List<TbChiTietHangCuuTro>();

    public virtual ICollection<TbChiTietHangUngHo> TbChiTietHangUngHos { get; set; } = new List<TbChiTietHangUngHo>();
}
