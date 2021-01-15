using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoaiSPsController : Controller
    {
        private readonly DataProviderContext _context;

        public LoaiSPsController(DataProviderContext context)
        {
            _context = context;
        }

        // GET: Admin/LoaiSPs
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (Request.QueryString.Value.IndexOf("s_name") < 0)
            {
                ViewBag.ListLSP = _context.LoaiSPs.ToList();
            }
            base.OnActionExecuted(context);
        }
        public async Task<IActionResult> Index(int? id, string? s_name, string? s_stt)
        {
            LoaiSP loaiSP = null;
            if (id != null)
            {
                loaiSP = await _context.LoaiSPs
                   .FirstOrDefaultAsync(m => m.MaLoai == id);
            }
            if (s_name != null)
            {
                if (s_stt == null)
                {
                    ViewBag.ListLSP = (from p in _context.LoaiSPs
                                       where p.TenLoai.IndexOf(s_name) >= 0
                                       select p).ToList();
                }
                else
                {
                    ViewBag.ListLSP = (from p in _context.LoaiSPs
                                       where p.TenLoai.IndexOf(s_name) >= 0 &&
                                       p.TrangThai==Convert.ToBoolean(s_stt)
                                       select p).ToList();
                }
            }
            return View(loaiSP);
        }

        // GET: Admin/LoaiSPs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSP = await _context.LoaiSPs
                .FirstOrDefaultAsync(m => m.MaLoai == id);
            if (loaiSP == null)
            {
                return NotFound();
            }

            return View(loaiSP);
        }

        // GET: Admin/LoaiSPs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiSPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoai,TenLoai,TrangThai")] LoaiSP loaiSP)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiSP);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index");
        }

        // GET: Admin/LoaiSPs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSP = await _context.LoaiSPs.FindAsync(id);
            if (loaiSP == null)
            {
                return NotFound();
            }
            return View("Index");
        }

        // POST: Admin/LoaiSPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLoai,TenLoai,TrangThai")] LoaiSP loaiSP)
        {
            if (id != loaiSP.MaLoai)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiSP);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiSPExists(loaiSP.MaLoai))
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

        // GET: Admin/LoaiSPs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSP = await _context.LoaiSPs
                .FirstOrDefaultAsync(m => m.MaLoai == id);
            if (loaiSP == null)
            {
                return NotFound();
            }

            return View(loaiSP);
        }

        // POST: Admin/LoaiSPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiSP = await _context.LoaiSPs.FindAsync(id);
            _context.LoaiSPs.Remove(loaiSP);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiSPExists(int id)
        {
            return _context.LoaiSPs.Any(e => e.MaLoai == id);
        }
    }
}
