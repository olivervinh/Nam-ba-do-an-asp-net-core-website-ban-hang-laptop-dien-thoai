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
    public class LoaiSP_APIController : ControllerBase
    {
        private readonly DataProviderContext _context;

        public LoaiSP_APIController(DataProviderContext context)
        {
            _context = context;
        }
        public class LoaiSP_Update_STT
        {
            public int ma { get; set; }
            public bool stt { get; set; }
        }
        [HttpPost]
        public string UpdateStatus(LoaiSP_Update_STT req)
        {
            (from p in _context.LoaiSPs
             where p.MaLoai == req.ma
             select p).ToList().ForEach(x => x.TrangThai = req.stt);
            _context.SaveChanges();
            return "{\"id\":" + req.ma + ",\"stt\":\"" + req.stt + "\"}";
        }
        // GET: api/LoaiSP_API
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoaiSP>>> GetLoaiSPs()
        {
            return await _context.LoaiSPs.ToListAsync();
        }

        // GET: api/LoaiSP_API/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoaiSP>> GetLoaiSP(int id)
        {
            var loaiSP = await _context.LoaiSPs.FindAsync(id);

            if (loaiSP == null)
            {
                return NotFound();
            }

            return loaiSP;
        }

        // PUT: api/LoaiSP_API/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<string> PutLoaiSP(int id, LoaiSP loaiSP)
        {
            if (id != loaiSP.MaLoai)
            {
                return "0";
            }

            _context.Entry(loaiSP).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoaiSPExists(id))
                {
                    return "0";
                }
                else
                {
                    throw;
                }
            }

            return "{\"id\":" + id + ",\"stt\":\"" + loaiSP.TrangThai + "\"}";
        }

        // POST: api/LoaiSP_API
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<LoaiSP>> PostLoaiSP(LoaiSP loaiSP)
        {
            _context.LoaiSPs.Add(loaiSP);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoaiSP", new { id = loaiSP.MaLoai }, loaiSP);
        }

        // DELETE: api/LoaiSP_API/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LoaiSP>> DeleteLoaiSP(int id)
        {
            var loaiSP = await _context.LoaiSPs.FindAsync(id);
            if (loaiSP == null)
            {
                return NotFound();
            }

            _context.LoaiSPs.Remove(loaiSP);
            await _context.SaveChangesAsync();

            return loaiSP;
        }

        private bool LoaiSPExists(int id)
        {
            return _context.LoaiSPs.Any(e => e.MaLoai == id);
        }
        
    }
}
