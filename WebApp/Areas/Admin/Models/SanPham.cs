using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace WebApp.Areas.Admin.Models
{
    public class SanPham
    {
        [Key]
        public int Ma { get; set; }
        [Required]
        Random RandomCls = new Random();
        [RegularExpression(@"^[0-9]{3}-[0-9]{3}-[0-9]{3}$")]
        [Display(Name = "Số Serial")]
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string SN { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Tên sản phẩm")]
        public string TenSP { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}",ApplyFormatInEditMode =true)]
        public DateTime NgaySX { get; set; }
        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        [Display(Name = "Hình")]
        public string Hinh { get; set; }
        [Display(Name ="Mô tả")]
        public string MoTa { get; set; }
        [System.ComponentModel.DataAnnotations.Range(1000, 1000000)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Giá")]
        public decimal Gia { get; set; }
        [Display(Name = "Trạng thái")]
        public bool TrangThai { get; set; }
        [Display(Name = "Loại")]
        public int MaLoai { get; set; }
        [ForeignKey("MaLoai")]
        public virtual LoaiSP LoaiSP { get; set; } 
    }
}
