using System;
using System.Collections.Generic;

namespace DAPM.Models;

public partial class TbChiTietHangCuuTro
{
    public long IdHangHoa { get; set; }

    public long IdTaiKhoan { get; set; }

    public long IdDotCuuTro { get; set; }

    public int SoLuong { get; set; }

    public virtual TbDotCuuTro IdDotCuuTroNavigation { get; set; } = null!;

    public virtual TbHangHoa IdHangHoaNavigation { get; set; } = null!;

    public virtual TbTaiKhoan IdTaiKhoanNavigation { get; set; } = null!;
}
