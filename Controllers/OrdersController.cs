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
    public class OrdersController : ControllerBase
    {
        private readonly SubcriptionMilkContext _context;
        private readonly IGetallService _service;
        public OrdersController(SubcriptionMilkContext context, IGetallService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/Orders
        [HttpGet("Getallorder")]
        public IActionResult GetAllList(string search)
        {
            var resulttest = _context.Orders.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                resulttest = _context.Orders.Where(us => us.Day.ToString().Equals(search));

            }

            var realresult = resulttest.Select(rs => new Order
            {
                Id = rs.Id,
                PacakeOrderId=rs.PacakeOrderId,
                SlotId =rs.SlotId,
                CollectionId =rs.CollectionId,
                DeliveryTripId =rs.DeliveryTripId,
                Day =rs.Day,
                Status =rs.Status
            });
            return Ok(new { StatusCode = 200, Message = "Load successful", data = realresult });
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        public async Task<IActionResult> PutOrder(Order order)
        {

            try
            {
                var ord = await _context.Orders.FindAsync(order.Id);
                if (ord == null)
                {
                    return NotFound();
                }
                ord.SlotId = order.SlotId;
                ord.CollectionId = order.CollectionId;
                ord.DeliveryTripId = order.DeliveryTripId;
                ord.Day = order.Day;
                ord.Status = order.Status; 
                _context.Orders.Update(ord);
                await _context.SaveChangesAsync();

                return Ok(new { StatusCode = 201, Message = "Update Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(new { StatusCode = 200, Content = "The Order was deleted successfully!!" });
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
