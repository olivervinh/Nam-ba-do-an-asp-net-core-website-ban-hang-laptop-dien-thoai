using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;

namespace WebApp.Areas.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Thuonghieu_APIController : ControllerBase
    {
        private readonly DataProviderContext _context;

        public Thuonghieu_APIController(DataProviderContext context)
        {
            _context = context;
        }

        // GET: api/Thuonghieu_api
        public class Thuonghieu_Update_STT
        {
            public int ma { get; set; }
            public bool stt { get; set; }
        }
        [HttpPost]
        public string UpdateStatus(Thuonghieu_Update_STT req)
        {
            (from p in _context.LoaiSPs
             where p.MaLoai == req.ma
             select p).ToList().ForEach(x => x.TrangThai = req.stt);
            _context.SaveChanges();
            return "{\"id\":" + req.ma + ",\"stt\":\"" + req.stt + "\"}";
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Thuonghieu>>> GetThuonghieus()
        {
            return await _context.Thuonghieus.ToListAsync();
        }

        // GET: api/Thuonghieu_api/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Thuonghieu>> GetThuonghieu(int id)
        {
            var thuonghieu = await _context.Thuonghieus.FindAsync(id);

            if (thuonghieu == null)
            {
                return NotFound();
            }

            return thuonghieu;
        }

        // PUT: api/Thuonghieu_api/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutThuonghieu(int id, Thuonghieu thuonghieu)
        {
           
            if (id != thuonghieu.MaThuonghieu)
            {
                return BadRequest();
            }

            _context.Entry(thuonghieu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThuonghieuExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Thuonghieu_api
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Thuonghieu>> PostThuonghieu(Thuonghieu thuonghieu)
        {
            _context.Thuonghieus.Add(thuonghieu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetThuonghieu", new { id = thuonghieu.MaThuonghieu }, thuonghieu);
        }

        // DELETE: api/Thuonghieu_api/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Thuonghieu>> DeleteThuonghieu(int id)
        {
            var thuonghieu = await _context.Thuonghieus.FindAsync(id);
            if (thuonghieu == null)
            {
                return NotFound();
            }

            _context.Thuonghieus.Remove(thuonghieu);
            await _context.SaveChangesAsync();

            return thuonghieu;
        }

        private bool ThuonghieuExists(int id)
        {
            return _context.Thuonghieus.Any(e => e.MaThuonghieu == id);
        }
    }
}
