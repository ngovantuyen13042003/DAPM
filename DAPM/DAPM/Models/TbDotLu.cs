using System;
using System.Collections.Generic;

namespace DAPM.Models;

public partial class TbDotLu
{
    public long IdDotLu { get; set; }

    public string TenDotLu { get; set; } = null!;

    public DateTime NgayBatDau { get; set; }

    public DateTime NgayKetThuc { get; set; }

    public virtual ICollection<TbBaiDang> TbBaiDangs { get; set; } = new List<TbBaiDang>();

    public virtual ICollection<TbChitietMucDoThietHai> TbChitietMucDoThietHais { get; set; } = new List<TbChitietMucDoThietHai>();

    public virtual ICollection<TbDonDangKyUngHo> TbDonDangKyUngHos { get; set; } = new List<TbDonDangKyUngHo>();

    public virtual ICollection<TbDotCuuTro> TbDotCuuTros { get; set; } = new List<TbDotCuuTro>();
}
