using System;
using System.Collections.Generic;

namespace DAPM.Models;

public partial class TbThongBao
{
    public long IdThongBao { get; set; }

    public string NoiDung { get; set; } = null!;

    public DateTime? NgayDang { get; set; }

    public long? IdTaiKhoan { get; set; }

    public virtual TbTaiKhoan? IdTaiKhoanNavigation { get; set; }

    public virtual ICollection<TbFile> TbFiles { get; set; } = new List<TbFile>();
}
