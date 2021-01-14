using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Models
{
    public class Donhang
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]

        public DateTime Datecheckout { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Email khách")]
      
        public string EmailKhach { set; get; }
   
       
        public double TongTienDonHang { set; get; }
        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Địa chỉ")]
        public string DiaChiShipping { set; get; }
     
        public int MaCartItem { get; set; }
        [ForeignKey(" MaCartItem")]
        public virtual CartItem CartItem { get; set; }
     
        public string MaUser { get; set; }
        [ForeignKey("MaUser")]

        public virtual IdentityUser IUser { get; set; }
        public ICollection<ChitietDonHang> LstDH { get; set; }
    }
}
