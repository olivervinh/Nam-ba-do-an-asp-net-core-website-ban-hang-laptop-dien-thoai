using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Models
{
    public class ChitietDonHang
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Tên sản phẩm")]
        public string TenSPBill { get; set; }
        public int Quantity { get; set; }
        public double ThanhTien { get; set; }
        public int Madonhang { get; set; }
        [ForeignKey("Madonhang")]
        public virtual Donhang Dh { get; set; }

    }
}
