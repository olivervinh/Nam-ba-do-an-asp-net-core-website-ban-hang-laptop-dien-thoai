using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly DataProviderContext _context;

        public HomeController(DataProviderContext context)
        {
            _context = context;
        }
        //hiển thị category 
        public IActionResult _homePartial()
        {
           
            var sp = from s in _context.SanPhams
                     join c in _context.LoaiSPs
                     on s.MaLoai equals c.MaLoai
                     
                     select new SanPham()
                     {
                         MaLoai=c.MaLoai,
                         LoaiSP= new LoaiSP { TenLoai=c.TenLoai,MaLoai=c.MaLoai,TrangThai=c.TrangThai}, 
                    
                         Ma=s.Ma, 
                         TenSP=s.TenSP,
                         Hinh=s.Hinh,
                         Gia=s.Gia,
                         MoTa=s.MoTa,
                         NgaySX=s.NgaySX,
                         SN=s.SN,
                        TrangThai=s.TrangThai,

                     };
           
            return View(sp.ToList());
        }
        public IActionResult _categoryPartial()
        {
            return View(_context.LoaiSPs);
        }
        public IActionResult _searchfollowCategoryPartial()
        {
            return View(_context.LoaiSPs);
        }
        public IActionResult _thanhbentraiCategory()
        {
            return View(_context.LoaiSPs);
        }
    }
}
