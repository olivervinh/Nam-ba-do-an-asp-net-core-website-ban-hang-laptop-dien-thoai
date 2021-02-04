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

        public DateTime Datecheckout { get; set; } = DateTime.Now;
       
        public string MaUser { get; set; }
        [ForeignKey("MaUser")]
       
        public virtual NguoiDung IUser { get; set; }
        public decimal TongTien { get; set; }
        public ICollection<ChitietDonHang> LstDH { get; set; }
    }
}
