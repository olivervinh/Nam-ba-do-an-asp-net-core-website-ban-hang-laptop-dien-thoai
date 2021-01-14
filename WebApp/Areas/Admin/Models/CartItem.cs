using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public int quantity { set; get; }
        public double ThanhTien { set; get; }
        public double TongTien { set; get; }

        public int MaSP { get; set; }
        [ForeignKey("MaSP")]
        public virtual SanPham product { set; get; }
    }
}
