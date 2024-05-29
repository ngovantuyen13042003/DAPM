using System;
using System.Collections.Generic;

namespace DAPM.Models;

public partial class TbMucDoThietHai
{
    public long IdMucDo { get; set; }

    public string MucThietHai { get; set; } = null!;

    public string? MoTa { get; set; }

    public virtual ICollection<TbChitietMucDoThietHai> TbChitietMucDoThietHais { get; set; } = new List<TbChitietMucDoThietHai>();
}
