using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubscriptionMilk.Models;
using SubscriptionMilk.Services;

namespace SubscriptionMilk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private readonly SubcriptionMilkContext _context;
        private readonly IGetallService _service;

        public StationsController(SubcriptionMilkContext context, IGetallService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/Stations
        [HttpGet("Getallstations")]
        public IActionResult GetAllList(string search)
        {
            try
            {
                var result = _service.GetAllStation(search);
                var totalitems = result.Count();

                return Ok(new { StatusCode = 200, Message = "Load successful", data = result, totalitems });
            }
            catch (Exception e)
            {

                return StatusCode(409, new { StatusCode = 409, Message = e.Message });

            }
        }

        // GET: api/Stations/5
        [HttpGet("getbyid")]
        public async Task<ActionResult> Getstation(int id)
        {
            var all = _context.Stations.AsQueryable();

            all = _context.Stations.Where(us => us.Id.Equals(id));
            var result = all.ToList();

            return Ok(new { StatusCode = 200, Message = "Load successful", data = result });
        }

        // PUT: api/Stations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        public async Task<IActionResult> PutStation(Station sta)
        {

            try
            {
                var station = await _context.Stations.FindAsync(sta.Id);
                if (station == null)
                {
                    return NotFound();
                }
                station.Id = sta.Id;
                station.Title = sta.Title;
                station.Description = sta.Description;
                station.Address = sta.Address;
                station.SlotId = sta.SlotId;


                _context.Stations.Update(station);
                await _context.SaveChangesAsync();

                return Ok(new { StatusCode = 201, Message = "Update Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/Stations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        public async Task<ActionResult<Station>> PostStation(Station sta)
        {
            try
            {
                var station = new Station();
                {
                    station.Id = sta.Id;
                    station.Title = sta.Title;
                    station.Description = sta.Description;
                    station.Address = sta.Address;
                    station.SlotId = sta.SlotId;
                }
                _context.Stations.Add(station);
                await _context.SaveChangesAsync();


                return Ok(new { StatusCode = 201, Message = "Add Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // DELETE: api/Stations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStation(int id)
        {
            var station = await _context.Stations.FindAsync(id);
            if (station == null)
            {
                return NotFound();
            }

            _context.Stations.Remove(station);
            await _context.SaveChangesAsync();

            return Ok(new { StatusCode = 200, Content = "The Station was deleted successfully!!" });
        }

        private bool StationExists(int id)
        {
            return _context.Stations.Any(e => e.Id == id);
        }
    }
}
