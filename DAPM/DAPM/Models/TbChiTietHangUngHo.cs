using System;
using System.Collections.Generic;

namespace DAPM.Models;

public partial class TbChiTietHangUngHo
{
    public long IdHangHoa { get; set; }

    public long IdDonDk { get; set; }

    public int? SoLuong { get; set; }

    public virtual TbDonDangKyUngHo IdDonDkNavigation { get; set; } = null!;

    public virtual TbHangHoa IdHangHoaNavigation { get; set; } = null!;
}
