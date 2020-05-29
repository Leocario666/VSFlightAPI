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
    public class FlightSetsController : ControllerBase
    {
        private readonly CoreDbContext _context;

        public FlightSetsController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: api/FlightSets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightSet>>> GetFlightSet()
        {
            return await _context.FlightSet.ToListAsync();
        }

        // GET: api/FlightSets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FlightSet>> GetFlightSet(int id)
        {
            var flightSet = await _context.FlightSet.FindAsync(id);

            if (flightSet == null)
            {
                return NotFound();
            }

            return flightSet;
        }

        // PUT: api/FlightSets/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlightSet(int id, FlightSet flightSet)
        {
            if (id != flightSet.FlightNo)
            {
                return BadRequest();
            }

            _context.Entry(flightSet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightSetExists(id))
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

        // POST: api/FlightSets
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FlightSet>> PostFlightSet(FlightSet flightSet)
        {
            _context.FlightSet.Add(flightSet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlightSet", new { id = flightSet.FlightNo }, flightSet);
        }

        // DELETE: api/FlightSets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FlightSet>> DeleteFlightSet(int id)
        {
            var flightSet = await _context.FlightSet.FindAsync(id);
            if (flightSet == null)
            {
                return NotFound();
            }

            _context.FlightSet.Remove(flightSet);
            await _context.SaveChangesAsync();

            return flightSet;
        }

        private bool FlightSetExists(int id)
        {
            return _context.FlightSet.Any(e => e.FlightNo == id);
        }
    }
}
