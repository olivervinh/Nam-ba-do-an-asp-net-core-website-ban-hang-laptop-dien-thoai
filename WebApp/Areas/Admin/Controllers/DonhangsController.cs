using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DonhangsController : Controller
    {
        private readonly DataProviderContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public DonhangsController(DataProviderContext context)
        {
            _context = context;
        }

        // GET: Admin/Donhangs
        public async Task<IActionResult> Index()
        {
            var dataProviderContext = _context.DonHangs.Include(d => d.IUser);
            return View(await dataProviderContext.ToListAsync());
        }

        // GET: Admin/Donhangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donhang = await _context.DonHangs
                .Include(d => d.IUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donhang == null)
            {
                return NotFound();
            }

            return View(donhang);
        }

        // GET: Admin/Donhangs/Create
        public async Task<IActionResult> Create()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewData["MaUser"] = currentUser.UserName;
            return View();
        }

        // POST: Admin/Donhangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Datecheckout,MaUser,TongTien")] Donhang donhang)
        {
            if (ModelState.IsValid)
            {
              
                _context.Add(donhang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewData["MaUser"] = currentUser.UserName;
            return View(donhang);
        }

        // GET: Admin/Donhangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donhang = await _context.DonHangs.FindAsync(id);
            if (donhang == null)
            {
                return NotFound();
            }
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewData["MaUser"] = currentUser.UserName;
            return View(donhang);
        }

        // POST: Admin/Donhangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Datecheckout,MaUser,TongTien")] Donhang donhang)
        {
            if (id != donhang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donhang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonhangExists(donhang.Id))
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
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewData["MaUser"] = currentUser.UserName;
            return View(donhang);
        }

        // GET: Admin/Donhangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donhang = await _context.DonHangs
                .Include(d => d.IUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donhang == null)
            {
                return NotFound();
            }

            return View(donhang);
        }

        // POST: Admin/Donhangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donhang = await _context.DonHangs.FindAsync(id);
            _context.DonHangs.Remove(donhang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonhangExists(int id)
        {
            return _context.DonHangs.Any(e => e.Id == id);
        }
    }
}
