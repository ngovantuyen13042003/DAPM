using System;
using System.Collections.Generic;

namespace DAPM.Models;

public partial class TbFile
{
    public long IdFile { get; set; }

    public string? UrlFile { get; set; }

    public long? IdThongBao { get; set; }

    public virtual TbThongBao? IdThongBaoNavigation { get; set; }
}
