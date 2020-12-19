using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamsController : Controller
    {
        private readonly DataProviderContext _context;

        public SanPhamsController(DataProviderContext context)
        {
            _context = context;
        }

        // GET: Admin/SanPhams
        public async Task<IActionResult> Index(string search, int Id)
        {
            Random rand = new Random();
            ViewData["rand"] = rand.Next().ToString();
            //Ket bang va timm kiem theo Id
            ViewBag.ListLoaiSP = _context.LoaiSPs.ToList();
            var KetBang = from s in _context.SanPhams
                          join l in _context.LoaiSPs
                          on s.MaLoai equals l.MaLoai
                          select new { s.Ma, s.TenSP, s.Hinh, s.MoTa, s.NgaySX,s.SN,s.MaLoai,s.TrangThai,s.Gia };
            //Tim kiem chuoi truy van 
            if (!String.IsNullOrEmpty(search))
            {
                KetBang = KetBang.Where(s => s.TenSP.Contains(search));
            }
            //Tim kiem theo id
            if (Id != 0)
            {
                KetBang = KetBang.Where(x => x.Ma == Id);
            }

            List<SanPham> ListSP = new List<SanPham>();
            foreach (var items in KetBang)
            {
                SanPham sp = new SanPham();
                sp.Ma = items.Ma;
                sp.TenSP = items.TenSP;
                sp.Hinh = items.Hinh;
                sp.SN = items.SN;
                sp.MaLoai = items.MaLoai;
                sp.TrangThai = sp.TrangThai;
                sp.Gia = items.Gia;
                ListSP.Add(sp);
            }
            return View(ListSP);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.LoaiSP)
                .FirstOrDefaultAsync(m => m.Ma == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }
        // GET: Admin/SanPhams/Create
        public IActionResult Create()
        {
            ViewData["MaLoai"] = new SelectList(_context.LoaiSPs, "MaLoai", "TenLoai");
            return View();
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ma,SN,TenSP,NgaySX,Hinh,MoTa,Gia,TrangThai,MaLoai")] SanPham sanPham, IFormFile ful)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sanPham); //Chi moi add vao context chu chua luu vao db
                await _context.SaveChangesAsync();//luu vao db

                //luu file vao trong duong dan
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot/img/pro",
                    sanPham.Ma + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]); //cat duoi file (vd: jpg)

                //copy file day
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await ful.CopyToAsync(stream);
                }

                //cap nhat lai ten
                sanPham.Hinh = sanPham.Ma + "." + ful.FileName.Split(".")
                    [ful.FileName.Split(".").Length - 1];

                //cap nhat vao db
                _context.Update(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.LoaiSPs, "MaLoai", "TenLoai", sanPham.MaLoai);
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewData["MaLoai"] = new SelectList(_context.LoaiSPs, "MaLoai", "TenLoai", sanPham.MaLoai);
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Ma,SN,TenSP,NgaySX,Hinh,MoTa,Gia,TrangThai,MaLoai")] SanPham sanPham, IFormFile ful)
        {
            if (id != sanPham.Ma)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ful != null)
                    {
                        //luu file vao trong duong dan
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/pro", sanPham.Hinh);

                        path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/pro",
                  sanPham.Ma + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]); //cat duoi file (vd: jpg)


                        //copy file day
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await ful.CopyToAsync(stream);
                        }

                        //cap nhat lai ten
                        sanPham.Hinh = sanPham.Ma + "." + ful.FileName.Split(".")
                            [ful.FileName.Split(".").Length - 1];
                    }

                    //luu file vao db
                    _context.Update(sanPham);
                    _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.Ma))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.LoaiSPs, "MaLoai", "TenLoai", sanPham.MaLoai);
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.LoaiSP)
                .FirstOrDefaultAsync(m => m.Ma == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: Admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);
            _context.SanPhams.Remove(sanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPhams.Any(e => e.Ma == id);
        }
    }
}
