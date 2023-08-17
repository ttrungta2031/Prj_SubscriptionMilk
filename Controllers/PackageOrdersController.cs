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
    public class PackageOrdersController : ControllerBase
    {
        private readonly SubcriptionMilkContext _context;
        private readonly IGetallService _service;
        public PackageOrdersController(SubcriptionMilkContext context, IGetallService service)
        {
            _context = context;
            _service = service;
        }
        //Getallpackageorder
        // GET: api/PackageOrders
        [HttpGet("Getallpackageorder")]
        public IActionResult GetAllList(string search)
        {
            try
            {
                var result = _service.GetAllPackageorder(search);
                var totalitems = result.Count();

                return Ok(new { StatusCode = 200, Message = "Load successful", data = result, totalitems });
            }
            catch (Exception e)
            {

                return StatusCode(409, new { StatusCode = 409, Message = e.Message });

            }
        }

        // GET: api/PackageOrders/5
        [HttpGet("getbyid")]
        public async Task<ActionResult> Getpackageorder(int id)
        {
            var all = _context.PackageOrders.AsQueryable();

            all = _context.PackageOrders.Where(us => us.Id.Equals(id));
            var result = all.ToList();

            return Ok(new { StatusCode = 200, Message = "Load successful", data = result });
        }

        // PUT: api/PackageOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        public async Task<IActionResult> PutPackageOrder(PackageOrder pack)
        {

            try
            {
                var package = await _context.PackageOrders.FindAsync(pack.Id);
                if (pack == null)
                {
                    return NotFound();
                }
                package.StartTime = pack.StartTime;
                package.EndTime = pack.EndTime;
                package.FullName = pack.FullName;
                package.StationId = pack.StationId;
                package.Phone = pack.Phone;
                package.Email = pack.Email;
                package.Description = pack.Description;
                package.PaymentId = pack.PaymentId;
                package.CustomerId = pack.CustomerId;
                package.PackageId = pack.PackageId;
                package.Total = pack.Total;


                _context.PackageOrders.Update(pack);
                await _context.SaveChangesAsync();

                return Ok(new { StatusCode = 201, Message = "Update Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/PackageOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        public async Task<ActionResult<PackageOrder>> PostPackageOrder(PackageOrder pack)
        {
            try
            {
                var package = new PackageOrder();
                {
                    package.Id = pack.Id;
                    package.StartTime = pack.StartTime;
                    package.EndTime = pack.EndTime;
                    package.FullName = pack.FullName;
                    package.StationId = pack.StationId;
                    package.Phone = pack.Phone;
                    package.Email = pack.Email;
                    package.Description = pack.Description;
                    package.PaymentId = pack.PaymentId;
                    package.CustomerId = pack.CustomerId;
                    package.PackageId = pack.PackageId;
                    package.Total = pack.Total;
                }
                _context.PackageOrders.Add(package);

                for (int i = 1; i <= 30; i++)
                {
                    var ord = new Order();
                    {

                        ord.PacakeOrderId = pack.Id;
                    }
                    _context.Orders.Add(ord);

                }


                await _context.SaveChangesAsync();
                return Ok(new { StatusCode = 201, Message = "Add Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // DELETE: api/PackageOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackageOrder(int id)
        {
            var pack = await _context.PackageOrders.FindAsync(id);
            if (pack == null)
            {
                return NotFound();
            }

            _context.PackageOrders.Remove(pack);
            await _context.SaveChangesAsync();

            return Ok(new { StatusCode = 200, Content = "The PackageOrders was deleted successfully!!" });
        }

        private bool PackageOrderExists(int id)
        {
            return _context.PackageOrders.Any(e => e.Id == id);
        }
    }
}
