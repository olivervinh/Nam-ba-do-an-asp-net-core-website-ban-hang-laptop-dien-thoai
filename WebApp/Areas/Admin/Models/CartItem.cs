using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Models
{
    public class CartItem
    {
        [Key]
      public int Id { get; set; }
        public int quantity { set; get; }
        public SanPham product { set; get; }
    }
}
