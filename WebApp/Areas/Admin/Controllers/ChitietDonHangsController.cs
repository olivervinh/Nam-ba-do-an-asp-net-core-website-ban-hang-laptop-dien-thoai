using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChitietDonHangsController : Controller
    {
        private readonly DataProviderContext _context;

        public ChitietDonHangsController(DataProviderContext context)
        {
            _context = context;
        }

        // GET: Admin/ChitietDonHangs
        public async Task<IActionResult> Index()
        {
            var dataProviderContext = _context.ChitietDonHangs.Include(c => c.Dh).Include(c => c.SP);
            return View(await dataProviderContext.ToListAsync());
        }

        // GET: Admin/ChitietDonHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chitietDonHang = await _context.ChitietDonHangs
                .Include(c => c.Dh)
                .Include(c => c.SP)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chitietDonHang == null)
            {
                return NotFound();
            }

            return View(chitietDonHang);
        }

        // GET: Admin/ChitietDonHangs/Create
        public IActionResult Create()
        {
            ViewData["Madonhang"] = new SelectList(_context.DonHangs, "Id", "Id");
            ViewData["MaSP"] = new SelectList(_context.SanPhams, "Ma", "TenSP");
            return View();
        }

        // POST: Admin/ChitietDonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity,ThanhTien,Madonhang,MaSP")] ChitietDonHang chitietDonHang)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(chitietDonHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Madonhang"] = new SelectList(_context.DonHangs, "Id", "Id", chitietDonHang.Madonhang);
            ViewData["MaSP"] = new SelectList(_context.SanPhams, "Ma", "TenSP", chitietDonHang.MaSP);
            return View(chitietDonHang);
        }

        // GET: Admin/ChitietDonHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chitietDonHang = await _context.ChitietDonHangs.FindAsync(id);
            if (chitietDonHang == null)
            {
                return NotFound();
            }
            ViewData["Madonhang"] = new SelectList(_context.DonHangs, "Id", "Id", chitietDonHang.Madonhang);
            ViewData["MaSP"] = new SelectList(_context.SanPhams, "Ma", "TenSP", chitietDonHang.MaSP);
            return View(chitietDonHang);
        }

        // POST: Admin/ChitietDonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantity,ThanhTien,Madonhang,MaSP")] ChitietDonHang chitietDonHang)
        {
            if (id != chitietDonHang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chitietDonHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChitietDonHangExists(chitietDonHang.Id))
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
            ViewData["Madonhang"] = new SelectList(_context.DonHangs, "Id", "Id", chitietDonHang.Madonhang);
            ViewData["MaSP"] = new SelectList(_context.SanPhams, "Ma", "TenSP", chitietDonHang.MaSP);
            return View(chitietDonHang);
        }

        // GET: Admin/ChitietDonHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chitietDonHang = await _context.ChitietDonHangs
                .Include(c => c.Dh)
                .Include(c => c.SP)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chitietDonHang == null)
            {
                return NotFound();
            }

            return View(chitietDonHang);
        }

        // POST: Admin/ChitietDonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chitietDonHang = await _context.ChitietDonHangs.FindAsync(id);
            _context.ChitietDonHangs.Remove(chitietDonHang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChitietDonHangExists(int id)
        {
            return _context.ChitietDonHangs.Any(e => e.Id == id);
        }
    }
}
