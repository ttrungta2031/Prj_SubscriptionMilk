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
    public class OrderDetailsController : ControllerBase
    {
        private readonly SubcriptionMilkContext _context;

        public OrderDetailsController(SubcriptionMilkContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails()
        {
            var all = _context.OrderDetails.AsQueryable();
            var result = all.Select(us => new OrderDetail
            {
                OrderId = us.OrderId,
                ProductId = us.ProductId,
               Amount = 1,

            });
           // return result.ToList();
            return Ok(new { StatusCode = 200, Message = "Load successful", data = result });
        }

        // GET: api/OrderDetails/5
        [HttpGet("getbyid")]
        public async Task<ActionResult> GetOrderdetail(int id)
        {
            var all = _context.OrderDetails.AsQueryable();

            all = _context.OrderDetails.Where(us => us.OrderId.Equals(id));
            var result = all.ToList();

            return Ok(new { StatusCode = 200, Message = "Load successful", data = result });
        }

        // PUT: api/OrderDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        public async Task<IActionResult> PutOrderdetails(OrderDetail ord)
        {

            try
            {
                var order = await _context.OrderDetails.FindAsync(ord.OrderId);
                if (order == null)
                {
                    return NotFound();
                }
                order.ProductId = ord.ProductId;
                order.OrderId = ord.OrderId;
                order.Amount = ord.Amount;
                order.Price = ord.Price;

                _context.OrderDetails.Update(order);
                await _context.SaveChangesAsync();

                return Ok(new { StatusCode = 201, Message = "Update Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/OrderDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        public async Task<ActionResult<OrderDetail>> PostOrderdetail(OrderDetail ord)
        {
            try
            {
                var order = new OrderDetail();
                {
                    order.ProductId = ord.ProductId;
                    order.OrderId = ord.OrderId;
                    order.Amount = ord.Amount;
                    order.Price = ord.Price;
                }
                _context.OrderDetails.Add(order);
                await _context.SaveChangesAsync();


                return Ok(new { StatusCode = 201, Message = "Add Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetails(int id)
        {
            var order = await _context.OrderDetails.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(new { StatusCode = 200, Content = "The OrderDetails was deleted successfully!!" });
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.ProductId == id);
        }
    }
}
