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
    public class DeliveryMenController : ControllerBase
    {
        private readonly SubcriptionMilkContext _context;
        private readonly IGetallService _service;
        public DeliveryMenController(SubcriptionMilkContext context, IGetallService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/DeliveryMen
        [HttpGet]
        public IActionResult GetAllList(string search)
        {
            try
            {
                var result = _service.GetAllDeliveryMan(search);
                var totalitems = result.Count();

                return Ok(new { StatusCode = 200, Message = "Load successful", data = result, totalitems });
            }
            catch (Exception e)
            {

                return StatusCode(409, new { StatusCode = 409, Message = e.Message });

            }
        }

        // GET: api/DeliveryMen/5
        [HttpGet("getbyid")]
        public async Task<ActionResult> GetDeliverymen(int id)
        {
            var all = _context.DeliveryMen.AsQueryable();

            all = _context.DeliveryMen.Where(us => us.Id.Equals(id));
            var result = all.ToList();

            return Ok(new { StatusCode = 200, Message = "Load successful", data = result });
        }

        // PUT: api/DeliveryMen/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        public async Task<IActionResult> PutDeliverymen(DeliveryMan dev)
        {

            try
            {
                var delive = await _context.DeliveryMen.FindAsync(dev.Id);
                if (delive == null)
                {
                    return NotFound();
                }
                delive.Id = dev.Id;
                delive.Img = dev.Img;
                delive.Username = dev.Username;
                delive.Password = dev.Password;
                delive.FullName = dev.FullName;
                delive.Phone = dev.Phone;


                _context.DeliveryMen.Update(delive);
                await _context.SaveChangesAsync();

                return Ok(new { StatusCode = 201, Message = "Update Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/DeliveryMen
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        public async Task<ActionResult<DeliveryMan>> PostDeliverymen(DeliveryMan dev)
        {
            try
            {
                var delive = new DeliveryMan();
                {
                    delive.Id = dev.Id;
                    delive.Img = dev.Img;
                    delive.Username = dev.Username;
                    delive.Password = dev.Password;
                    delive.FullName = dev.FullName;
                    delive.Phone = dev.Phone;
                }
                _context.DeliveryMen.Add(delive);
                await _context.SaveChangesAsync();


                return Ok(new { StatusCode = 201, Message = "Add Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // DELETE: api/DeliveryMen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliverymen(int id)
        {
            var delive = await _context.DeliveryMen.FindAsync(id);
            if (delive == null)
            {
                return NotFound();
            }

            _context.DeliveryMen.Remove(delive);
            await _context.SaveChangesAsync();

            return Ok(new { StatusCode = 200, Content = "The DeliveryMen was deleted successfully!!" });
        }

        private bool DeliveryManExists(int id)
        {
            return _context.DeliveryMen.Any(e => e.Id == id);
        }
    }
}
