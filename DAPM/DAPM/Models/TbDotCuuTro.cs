using System;
using System.Collections.Generic;

namespace DAPM.Models;

public partial class TbDotCuuTro
{
    public long IdDotCuuTro { get; set; }

    public string TenDotCuuTro { get; set; } = null!;

    public DateTime? NgayBatDau { get; set; }

    public DateTime? NgayKetThuc { get; set; }

    public long? IdDotLu { get; set; }

    public virtual TbDotLu? IdDotLuNavigation { get; set; }

    public virtual ICollection<TbChiTietHangCuuTro> TbChiTietHangCuuTros { get; set; } = new List<TbChiTietHangCuuTro>();
}
