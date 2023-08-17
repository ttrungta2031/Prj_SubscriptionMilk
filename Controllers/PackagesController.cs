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
    public class PackagesController : ControllerBase
    {
        private readonly SubcriptionMilkContext _context;
        private readonly IGetallService _service;
        public PackagesController(SubcriptionMilkContext context, IGetallService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/Packages
        [HttpGet("Getallpackages")]
        public IActionResult GetAllList(string search)
        {
            try
            {
                var result = _service.GetAllPackage(search);
                var totalitems = result.Count();

                return Ok(new { StatusCode = 200, Message = "Load successful", data = result, totalitems });
            }
            catch (Exception e)
            {

                return StatusCode(409, new { StatusCode = 409, Message = e.Message });

            }
        }

        // GET: api/Packages/5
        [HttpGet("getbyid")]
        public async Task<ActionResult> Getpackages(int id)
        {
            var all = _context.Packages.AsQueryable();

            all = _context.Packages.Where(us => us.Id.Equals(id));
            var result = all.ToList();

            return Ok(new { StatusCode = 200, Message = "Load successful", data = result });
        }

        // PUT: api/Packages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        public async Task<IActionResult> PutPackage(Package pack)
        {

            try
            {
                var package = await _context.Packages.FindAsync(pack.Id);
                if (package == null)
                {
                    return NotFound();
                }
                package.Img = pack.Img;
                package.Title = pack.Title;
                package.Description = pack.Description;
                package.Price = pack.Price;


                _context.Packages.Update(package);
                await _context.SaveChangesAsync();

                return Ok(new { StatusCode = 201, Message = "Update Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/Packages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        public async Task<ActionResult<Package>> PostPackage(Package pack)
        {
            try
            {
                var package = new Package();
                {
                    package.Id = pack.Id;
                    package.Img = pack.Img;
                    package.Title = pack.Title;
                    package.Description = pack.Description;
                    package.Price = pack.Price;
                }
                _context.Packages.Add(package);
                await _context.SaveChangesAsync();


                return Ok(new { StatusCode = 201, Message = "Add Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // DELETE: api/Packages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            var package = await _context.Packages.FindAsync(id);
            if (package == null)
            {
                return NotFound();
            }

            _context.Packages.Remove(package);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PackageExists(int id)
        {
            return _context.Packages.Any(e => e.Id == id);
        }
    }
}
