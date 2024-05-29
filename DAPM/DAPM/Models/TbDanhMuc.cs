using System;
using System.Collections.Generic;

namespace DAPM.Models;

public partial class TbDanhMuc
{
    public long IdDanhMuc { get; set; }

    public string TenDanhMuc { get; set; } = null!;

    public string? MoTa { get; set; }

    public virtual ICollection<TbHangHoa> TbHangHoas { get; set; } = new List<TbHangHoa>();
}
