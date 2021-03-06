﻿using System;
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
    public class StatsController : ControllerBase
    {
        private readonly CoreDbContext _context;

        public StatsController(CoreDbContext context)
        {
            _context = context;
        }
        // GET: api/Stats
        [HttpGet]
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }

        // Return the total price from 1 flight
        // GET: api/Stats/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<int> GetAsync(int id)
        {
            
            var cc = await _context.BookingSet.ToListAsync();
            var dd = await _context.FlightSet.ToListAsync();
            var q = from a in cc
                    where a.FlightNo.Equals(id)
                    select a;
            int finalvalue = 0;

            foreach (BookingSet a in q)
            {
                finalvalue += a.Price;
            }
            return finalvalue;
        }

        
        [HttpGet("GetAveragePriceForDestination/{Destination}")]
        public async Task<float> GetAveragePriceForDestination([FromRoute] String Destination)
        {
            float average = 0;
            var BookingList = await _context.BookingSet.ToListAsync();
            var FlightList = await _context.FlightSet.ToListAsync();
            var q = from a in FlightList
                    where a.Destination.Equals(Destination)
                    select a;
            
            List<BookingSet> list = new List<BookingSet>();
            foreach(BookingSet b in BookingList)
            {
                foreach(FlightSet f in q)
                {
                    if(f.FlightNo == b.FlightNo)
                    {
                        list.Add(b);
                    }
                }
            }

            foreach (BookingSet b in list)
            {
                average += b.Price;
            }
            average /= list.Count();
            return average;
        }

        
        [HttpGet("GetStats/{Destination}")]
        public async Task<List<Statistics>> GetStats([FromRoute] String Destination)
        {
            List<Statistics> stats = new List<Statistics>();

            var booklist = await _context.BookingSet.ToListAsync();
            var flightlist = await _context.FlightSet.ToListAsync();
            var passengerlist = await _context.PassengerSet.ToListAsync();
            var q = from a in flightlist
                    where a.Destination.Equals(Destination)
                    select a;

            List<BookingSet> list = new List<BookingSet>();
            foreach (BookingSet b in booklist)
            {
                foreach (FlightSet f in q)
                {
                    if (f.FlightNo == b.FlightNo)
                    {
                        list.Add(b);
                    }
                }
            }
            foreach (BookingSet bs in list)
            {

                Statistics s = new Statistics();
                foreach (PassengerSet ps in passengerlist)
                {
                    if (ps.PersonId == bs.PassengerId)
                    {
                        s.GivenName = ps.GivenName;

                    }
                }

                s.FlightNo = bs.FlightNo;
                s.Price = bs.Price;

                stats.Add(s);
            }
            return stats;
        }

        // POST: api/Stats
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Stats/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
