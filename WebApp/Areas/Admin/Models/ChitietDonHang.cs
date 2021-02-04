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
        public int Quantity { get; set; }
        public decimal ThanhTien { get; set; }
        public int Madonhang { get; set; }
        [ForeignKey("Madonhang")]
        public virtual Donhang Dh { get; set; }
        public int MaSP { get; set; }
      
        [ForeignKey("MaSP")]
        public virtual SanPham SP { get; set; }

    }
}
