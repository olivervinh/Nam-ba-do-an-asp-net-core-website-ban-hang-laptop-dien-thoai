using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace WebApp.Areas.Admin.Models 
{
    public class LoaiSP
    {
        [Key]
        public int MaLoai { get; set; }
        [Display(Name ="tên loại sản phẩm")]
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [StringLength(50,MinimumLength =3)]
       
        public string TenLoai { get; set; }
        public bool TrangThai { get; set; }
       
        public ICollection<SanPham> LstSanPhams { get; set; }
    }
}
