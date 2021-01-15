using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Models
{
    public class Thuonghieu
    {
        [Key]
        public int MaThuonghieu { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Tên thương hiệu")]
        public string TenTH { get; set; }
        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        [Display(Name = "Hình")]
        public string Hinh { get; set; }
        public bool Trangthai { get; set; }
        public ICollection<SanPham> LstSanPhams { get; set; }
    }
}
