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
    public class FishersController : ControllerBase
    {
        private readonly WebAppContext _context;

        public FishersController(WebAppContext context)
        {
            _context = context;
        }

        // GET: api/Fishers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fisher>>> GetFishers()
        {
            return await _context.Fishers.ToListAsync();
        }

        // GET: api/Fishers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fisher>> GetFisher(int id)
        {
            var fisher = await _context.Fishers.FindAsync(id);

            if (fisher == null)
            {
                return NotFound();
            }

            return fisher;
        }

        // PUT: api/Fishers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFisher(int id, Fisher fisher)
        {
            if (id != fisher.UserId)
            {
                return BadRequest();
            }

            _context.Entry(fisher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FisherExists(id))
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

        // POST: api/Fishers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Fisher>> PostFisher(Fisher fisher)
        {
            _context.Fishers.Add(fisher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFisher", new { id = fisher.UserId }, fisher);
        }

        // DELETE: api/Fishers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Fisher>> DeleteFisher(int id)
        {
            var fisher = await _context.Fishers.FindAsync(id);
            if (fisher == null)
            {
                return NotFound();
            }

            _context.Fishers.Remove(fisher);
            await _context.SaveChangesAsync();

            return fisher;
        }

        private bool FisherExists(int id)
        {
            return _context.Fishers.Any(e => e.UserId == id);
        }
    }
}
