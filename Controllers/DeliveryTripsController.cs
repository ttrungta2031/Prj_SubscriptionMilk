using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubscriptionMilk.Models;

namespace SubscriptionMilk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryTripsController : ControllerBase
    {
        private readonly SubcriptionMilkContext _context;

        public DeliveryTripsController(SubcriptionMilkContext context)
        {
            _context = context;
        }

        // GET: api/DeliveryTrips
        [HttpGet("Getalldeliverytrip")]
        public IActionResult GetAllList(string search)
        {
          var  result = (from s in _context.DeliveryTrips
                      select new
                      {
                          Id = s.Id,
                          DeliveryManId = s.DeliveryManId,
                          StoreId = s.StoreId,
                          StationId = s.StationId
                      }).ToList();

            if (!string.IsNullOrEmpty(search))
            {
                  result = (from s in _context.DeliveryTrips
                         where s.DeliveryManId.ToString().Contains(search)
                         select new
                         {
                             Id = s.Id,
                             DeliveryManId = s.DeliveryManId,
                             StoreId=s.StoreId,
                             StationId=s.StationId
                         }).ToList();
                }

            return Ok(new { StatusCode = 200, Message = "Load successful", data = result });
        }

        // GET: api/DeliveryTrips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryTrip>> GetDeliveryTrip(int id)
        {
            var deliveryTrip = await _context.DeliveryTrips.FindAsync(id);

            if (deliveryTrip == null)
            {
                return NotFound();
            }

            return deliveryTrip;
        }

        // PUT: api/DeliveryTrips/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeliveryTrip(int id, DeliveryTrip deliveryTrip)
        {
            if (id != deliveryTrip.Id)
            {
                return BadRequest();
            }

            _context.Entry(deliveryTrip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryTripExists(id))
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

        // POST: api/DeliveryTrips
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DeliveryTrip>> PostDeliveryTrip(DeliveryTrip deliveryTrip)
        {
            _context.DeliveryTrips.Add(deliveryTrip);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DeliveryTripExists(deliveryTrip.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDeliveryTrip", new { id = deliveryTrip.Id }, deliveryTrip);
        }

        // DELETE: api/DeliveryTrips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryTrip(int id)
        {
            var deliveryTrip = await _context.DeliveryTrips.FindAsync(id);
            if (deliveryTrip == null)
            {
                return NotFound();
            }

            _context.DeliveryTrips.Remove(deliveryTrip);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeliveryTripExists(int id)
        {
            return _context.DeliveryTrips.Any(e => e.Id == id);
        }
    }
}
