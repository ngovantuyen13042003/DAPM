using System;
using System.Collections.Generic;

namespace DAPM.Models;

public partial class TbChitietMucDoThietHai
{
    public long IdMucDo { get; set; }

    public long IdTaiKhoan { get; set; }

    public long IdDotLu { get; set; }

    public string? Mota { get; set; }

    public virtual TbDotLu IdDotLuNavigation { get; set; } = null!;

    public virtual TbMucDoThietHai IdMucDoNavigation { get; set; } = null!;

    public virtual TbTaiKhoan IdTaiKhoanNavigation { get; set; } = null!;
}
