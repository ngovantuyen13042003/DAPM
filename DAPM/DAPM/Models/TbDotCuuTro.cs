using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAPM.Models
{
    public partial class TbDotCuuTro
    {
        [Key]
        public long IdDotCuuTro { get; set; }

        [Required(ErrorMessage = "Tên đợt cứu trợ là bắt buộc.")]
        [Display(Name = "Tên Đợt Cứu Trợ")]
        public string TenDotCuuTro { get; set; } = null!;

        [Display(Name = "Ngày Bắt Đầu")]
        [DataType(DataType.Date)]
        public DateTime? NgayBatDau { get; set; }

        [Display(Name = "Ngày Kết Thúc")]
        [DataType(DataType.Date)]
        public DateTime? NgayKetThuc { get; set; }

        [ForeignKey("TbDotLu")]
        [Display(Name = "Đợt Lũ")]
        public long? IdDotLu { get; set; }

        public virtual TbDotLu? IdDotLuNavigation { get; set; }

        public virtual ICollection<TbChiTietHangCuuTro> TbChiTietHangCuuTros { get; set; } = new List<TbChiTietHangCuuTro>();
    }
}
