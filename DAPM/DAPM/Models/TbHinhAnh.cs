using System;
using System.Collections.Generic;

namespace DAPM.Models;

public partial class TbHinhAnh
{
    public long IdHinhAnh { get; set; }

    public string UrlHinhAnh { get; set; } = null!;

    public long? IdBaiDang { get; set; }

    public virtual TbBaiDang? IdBaiDangNavigation { get; set; }
}
