using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAPM.Models
{
    public partial class TbDotLu
    {
        [Key]
        public long IdDotLu { get; set; }

        [Required(ErrorMessage = "Tên đợt lũ là bắt buộc.")]
        [Display(Name = "Tên Đợt Lũ")]
        public string TenDotLu { get; set; } = null!;

        [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc.")]
        [Display(Name = "Ngày Bắt Đầu")]
        [DataType(DataType.Date)]
        public DateTime NgayBatDau { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc là bắt buộc.")]
        [Display(Name = "Ngày Kết Thúc")]
        [DataType(DataType.Date)]
        public DateTime NgayKetThuc { get; set; }

        public virtual ICollection<TbBaiDang> TbBaiDangs { get; set; } = new List<TbBaiDang>();
        public virtual ICollection<TbChitietMucDoThietHai> TbChitietMucDoThietHais { get; set; } = new List<TbChitietMucDoThietHai>();
        public virtual ICollection<TbDonDangKyUngHo> TbDonDangKyUngHos { get; set; } = new List<TbDonDangKyUngHo>();
        public virtual ICollection<TbDotCuuTro> TbDotCuuTros { get; set; } = new List<TbDotCuuTro>();
    }
}
