using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Models;
using WebApp.ViewModels;


namespace WebApp.Areas.Admin.Data
{
    public class DataProviderContext : IdentityDbContext<IdentityUser>
    {
       //Khởi tạo doi tượng 
       public DataProviderContext(DbContextOptions<DataProviderContext> options):base(options)
        {

        }
        public DbSet<Thuonghieu> Thuonghieus { get; set; }
        public DbSet<LoaiSP> LoaiSPs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<Member> Members { get; set; }
     
        public DbSet<Donhang> DonHangs { get; set; }
        public DbSet<ChitietDonHang> ChitietDonHangs{ get; set; }

        public DbSet<NguoiDung> NguoiDungs { get; set; }

       
    }
}
