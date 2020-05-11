using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RejectsController : ControllerBase
    {
        private readonly WebAppContext _context;

        public RejectsController(WebAppContext context)
        {
            _context = context;
        }

        // GET: api/Rejects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reject>>> GetRejects()
        {
            return await _context.Rejects.ToListAsync();
        }

        // GET: api/Rejects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reject>> GetReject(int id)
        {
            var reject = await _context.Rejects.FindAsync(id);

            if (reject == null)
            {
                return NotFound();
            }

            return reject;
        }

        // PUT: api/Rejects/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReject(int id, Reject reject)
        {
            if (id != reject.rejectId)
            {
                return BadRequest();
            }

            _context.Entry(reject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RejectExists(id))
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

        // POST: api/Rejects
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Reject>> PostReject(Reject reject)
        {
            _context.Rejects.Add(reject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReject", new { id = reject.rejectId }, reject);
        }

        // DELETE: api/Rejects/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reject>> DeleteReject(int id)
        {
            var reject = await _context.Rejects.FindAsync(id);
            if (reject == null)
            {
                return NotFound();
            }

            _context.Rejects.Remove(reject);
            await _context.SaveChangesAsync();

            return reject;
        }

        private bool RejectExists(int id)
        {
            return _context.Rejects.Any(e => e.rejectId == id);
        }
    }
}
