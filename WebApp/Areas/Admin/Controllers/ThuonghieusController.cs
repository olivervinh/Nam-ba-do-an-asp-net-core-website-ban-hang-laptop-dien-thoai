using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThuonghieusController : Controller
    {
        private readonly DataProviderContext _context;

        public ThuonghieusController(DataProviderContext context)
        {
            _context = context;
        }

        // GET: Admin/Thuonghieus
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (Request.QueryString.Value.IndexOf("s_name") < 0)
            {
                ViewBag.ListTH = _context.Thuonghieus.ToList();
            }
            base.OnActionExecuted(context);
        }
        public async Task<IActionResult> Index(int? id, string? s_name, string? s_stt)
        {
            Thuonghieu thuonghieu = null;
            if (id != null)
            {
              thuonghieu = await _context.Thuonghieus
                   .FirstOrDefaultAsync(m => m.MaThuonghieu == id);
            }
            if (s_name != null)
            {
                if (s_stt == null)
                {
                    ViewBag.ListLSP = (from p in _context.Thuonghieus
                                       where p.TenTH.IndexOf(s_name) >= 0
                                       select p).ToList();
                }
                else
                {
                    ViewBag.ListLSP = (from p in _context.Thuonghieus
                                       where p.TenTH.IndexOf(s_name) >= 0 &&
                                       p.Trangthai == Convert.ToBoolean(s_stt)
                                       select p).ToList();
                }
            }
            return View(thuonghieu);
        }

        // GET: Admin/Thuonghieus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thuonghieu = await _context.Thuonghieus
                .FirstOrDefaultAsync(m => m.MaThuonghieu == id);
            if (thuonghieu == null)
            {
                return NotFound();
            }

            return View(thuonghieu);
        }

        // GET: Admin/Thuonghieus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Thuonghieus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaThuonghieu,TenTH,Hinh,Trangthai")] Thuonghieu thuonghieu, IFormFile ful)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thuonghieu);
                await _context.SaveChangesAsync();
                var path = Path.Combine(
                 Directory.GetCurrentDirectory(), "wwwroot/img/pro/th",
                 thuonghieu.MaThuonghieu + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]); //cat duoi file (vd: jpg)

                //copy file day
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await ful.CopyToAsync(stream);
                }

                //cap nhat lai ten
                thuonghieu.Hinh = thuonghieu.MaThuonghieu + "." + ful.FileName.Split(".")
                    [ful.FileName.Split(".").Length - 1];

                //cap nhat vao db
                _context.Update(thuonghieu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

               
            }
            return View("Index");
        }

        // GET: Admin/Thuonghieus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thuonghieu = await _context.Thuonghieus.FindAsync(id);
            if (thuonghieu == null)
            {
                return NotFound();
            }
            return View("Index");
        }

        // POST: Admin/Thuonghieus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaThuonghieu,TenTH,Hinh,Trangthai")] Thuonghieu thuonghieu, IFormFile ful)
        {
            if (id != thuonghieu.MaThuonghieu)
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
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/pro/th");

                        path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/pro/th",
                  thuonghieu.MaThuonghieu + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]); //cat duoi file (vd: jpg)


                        //copy file day
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await ful.CopyToAsync(stream);
                        }

                        //cap nhat lai ten
                        thuonghieu.Hinh = thuonghieu.MaThuonghieu + "." + ful.FileName.Split(".")
                            [ful.FileName.Split(".").Length - 1];
                    }

                    //luu file vao db
                    _context.Update(thuonghieu);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThuonghieuExists(thuonghieu.MaThuonghieu))
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
            return View("Index");
        }

        // GET: Admin/Thuonghieus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thuonghieu = await _context.Thuonghieus
                .FirstOrDefaultAsync(m => m.MaThuonghieu == id);
            if (thuonghieu == null)
            {
                return NotFound();
            }

            return View(thuonghieu);
        }

        // POST: Admin/Thuonghieus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thuonghieu = await _context.Thuonghieus.FindAsync(id);
            _context.Thuonghieus.Remove(thuonghieu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThuonghieuExists(int id)
        {
            return _context.Thuonghieus.Any(e => e.MaThuonghieu == id);
        }
    }
}
