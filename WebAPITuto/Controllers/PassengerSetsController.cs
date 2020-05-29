using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPITuto.Models;

namespace WebAPITuto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerSetsController : ControllerBase
    {
        private readonly CoreDbContext _context;

        public PassengerSetsController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: api/PassengerSets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PassengerSet>>> GetPassengerSet()
        {
            return await _context.PassengerSet.ToListAsync();
        }

        // GET: api/PassengerSets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PassengerSet>> GetPassengerSet(int id)
        {
            var passengerSet = await _context.PassengerSet.FindAsync(id);

            if (passengerSet == null)
            {
                return NotFound();
            }

            return passengerSet;
        }

        // PUT: api/PassengerSets/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassengerSet(int id, PassengerSet passengerSet)
        {
            if (id != passengerSet.PersonId)
            {
                return BadRequest();
            }

            _context.Entry(passengerSet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PassengerSetExists(id))
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

        // POST: api/PassengerSets
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PassengerSet>> PostPassengerSet(PassengerSet passengerSet)
        {
            _context.PassengerSet.Add(passengerSet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPassengerSet", new { id = passengerSet.PersonId }, passengerSet);
        }

        // DELETE: api/PassengerSets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PassengerSet>> DeletePassengerSet(int id)
        {
            var passengerSet = await _context.PassengerSet.FindAsync(id);
            if (passengerSet == null)
            {
                return NotFound();
            }

            _context.PassengerSet.Remove(passengerSet);
            await _context.SaveChangesAsync();

            return passengerSet;
        }

        private bool PassengerSetExists(int id)
        {
            return _context.PassengerSet.Any(e => e.PersonId == id);
        }
    }
}
