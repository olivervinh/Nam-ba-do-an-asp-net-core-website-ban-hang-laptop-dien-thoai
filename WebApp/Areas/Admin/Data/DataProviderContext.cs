using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Models;

namespace WebApp.Areas.Admin.Data
{
    public class DataProviderContext: DbContext
    {
       //Khởi tạo doi tượng 
       public DataProviderContext(DbContextOptions<DataProviderContext> options):base(options)
        {

        }
        public DbSet<LoaiSP> LoaiSPs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<CartItem> CartItems { get; set; }


    }
}
