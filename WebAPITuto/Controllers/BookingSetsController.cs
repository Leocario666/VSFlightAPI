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
    public class BookingSetsController : ControllerBase
    {
        private readonly CoreDbContext _context;

        public BookingSetsController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: api/BookingSets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingSet>>> GetBookingSet()
        {
            return await _context.BookingSet.ToListAsync();
        }

        // GET: api/BookingSets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingSet>> GetBookingSet(int id)
        {
            var bookingSet = await _context.BookingSet.FindAsync(id);

            if (bookingSet == null)
            {
                return NotFound();
            }

            return bookingSet;
        }

        // PUT: api/BookingSets/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingSet(int id, BookingSet bookingSet)
        {
            if (id != bookingSet.FlightNo)
            {
                return BadRequest();
            }

            _context.Entry(bookingSet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingSetExists(id))
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

        // POST: api/BookingSets
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BookingSet>> PostBookingSet(BookingSet bookingSet)
        {
            _context.BookingSet.Add(bookingSet);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookingSetExists(bookingSet.FlightNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBookingSet", new { id = bookingSet.FlightNo }, bookingSet);
        }

        // DELETE: api/BookingSets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookingSet>> DeleteBookingSet(int id)
        {
            var bookingSet = await _context.BookingSet.FindAsync(id);
            if (bookingSet == null)
            {
                return NotFound();
            }

            _context.BookingSet.Remove(bookingSet);
            await _context.SaveChangesAsync();

            return bookingSet;
        }

        private bool BookingSetExists(int id)
        {
            return _context.BookingSet.Any(e => e.FlightNo == id);
        }
    }
}
