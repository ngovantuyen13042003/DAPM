using System;
using System.Collections.Generic;

namespace DAPM.Models;

public partial class TbBaiDang
{
    public long IdBaiDang { get; set; }

    public string TieuDe { get; set; } = null!;

    public string NoiDung { get; set; } = null!;

    public DateTime? NgayDang { get; set; }

    public long? IdDotLu { get; set; }

    public long? IdTaiKhoan { get; set; }

    public virtual TbDotLu? IdDotLuNavigation { get; set; }

    public virtual TbTaiKhoan? IdTaiKhoanNavigation { get; set; }

    public virtual ICollection<TbHinhAnh> TbHinhAnhs { get; set; } = new List<TbHinhAnh>();
}
