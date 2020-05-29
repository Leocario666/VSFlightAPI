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
    public class PilotSetsController : ControllerBase
    {
        private readonly CoreDbContext _context;

        public PilotSetsController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: api/PilotSets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PilotSet>>> GetPilotSet()
        {
            return await _context.PilotSet.ToListAsync();
        }

        // GET: api/PilotSets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PilotSet>> GetPilotSet(int id)
        {
            var pilotSet = await _context.PilotSet.FindAsync(id);

            if (pilotSet == null)
            {
                return NotFound();
            }

            return pilotSet;
        }

        // PUT: api/PilotSets/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPilotSet(int id, PilotSet pilotSet)
        {
            if (id != pilotSet.PersonId)
            {
                return BadRequest();
            }

            _context.Entry(pilotSet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PilotSetExists(id))
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

        // POST: api/PilotSets
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PilotSet>> PostPilotSet(PilotSet pilotSet)
        {
            _context.PilotSet.Add(pilotSet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPilotSet", new { id = pilotSet.PersonId }, pilotSet);
        }

        // DELETE: api/PilotSets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PilotSet>> DeletePilotSet(int id)
        {
            var pilotSet = await _context.PilotSet.FindAsync(id);
            if (pilotSet == null)
            {
                return NotFound();
            }

            _context.PilotSet.Remove(pilotSet);
            await _context.SaveChangesAsync();

            return pilotSet;
        }

        private bool PilotSetExists(int id)
        {
            return _context.PilotSet.Any(e => e.PersonId == id);
        }
    }
}
