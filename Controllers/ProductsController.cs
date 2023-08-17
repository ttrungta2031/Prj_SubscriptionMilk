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
    public class ProductsController : ControllerBase
    {
        private readonly SubcriptionMilkContext _context;

        public ProductsController(SubcriptionMilkContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet("Getallproduct")]
        public IActionResult GetAllList(string search, string filter)
        {
            /*var result = (from s in _context.Products
                          select new
                          {
                              Id = s.Id,
                              Img = s.Img,
                              Title = s.Title,
                              Description = s.Description,
                              Active = s.Active,
                              CategoryId = s.CategoryId,
                              SupplierId = s.SupplierId
                          }).ToList();

            if (!string.IsNullOrEmpty(filter))
            {
                result = (from s in _context.Products
                          where s.CategoryId.ToString().Contains(filter)
                          select new
                          {
                              Id = s.Id,
                              Img = s.Img,
                              Title = s.Title,
                              Description = s.Description,
                              Active = s.Active,
                              CategoryId = s.CategoryId,
                              SupplierId = s.SupplierId
                          }).ToList();
            }
            if (!string.IsNullOrEmpty(search))
            {
                result = (from s in _context.Products
                          where s.Id.ToString().Contains(search) || s.Img.ToString().Contains(search) || s.Title.ToString().Contains(search) || s.Description.ToString().Contains(search) || s.Active.ToString().Contains(search)
                          select new
                          {
                              Id = s.Id,
                              Img = s.Img,
                              Title = s.Title,
                              Description = s.Description,
                              Active = s.Active,
                              CategoryId = s.CategoryId,
                              SupplierId = s.SupplierId
                          }).ToList();
            }*/

            var resulttest = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                resulttest = _context.Products.Where(us => us.CategoryId.ToString().Contains(filter));

            }
            if (!string.IsNullOrEmpty(search) && string.IsNullOrEmpty(filter))
            {
                resulttest = _context.Products.Where(us =>(us.Title.Contains(search) || us.Img.Contains(search) || us.Description.Contains(search) || us.Active.Contains(search)));

            }

            if (!string.IsNullOrEmpty(search) && !string.IsNullOrEmpty(filter))
            {
                resulttest = _context.Products.Where(us => us.CategoryId.ToString().Contains(filter)  &&(us.Title.Contains(search) || us.Img.Contains(search) || us.Description.Contains(search) || us.Active.Contains(search)) );

            }
            var realresult = resulttest.Select(rs => new Product
            {
                Id = rs.Id,
                Img = rs.Img,
                Title = rs.Title,
                Description = rs.Description,
                Active = rs.Active,
                CategoryId = rs.CategoryId,
                SupplierId = rs.SupplierId
            });
            return Ok(new { StatusCode = 200, Message = "Load successful", data = realresult });
        }


        // GET: api/Products/5
        [HttpGet("getbyid")]
        public async Task<ActionResult> Getproduct(int id)
        {
            var all = _context.Products.AsQueryable();

            all = _context.Products.Where(us => us.Id.Equals(id));
            var result = all.ToList();

            return Ok(new { StatusCode = 200, Message = "Load successful", data = result });
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        public async Task<IActionResult> PutProduct(Product pro)
        {

            try
            {
                var product = await _context.Products.FindAsync(pro.Id);
                if (product == null)
                {
                    return NotFound();
                }
                product.Img = pro.Img;
                product.Title = pro.Title;
                product.Description = pro.Description;
                product.Active = pro.Active;
                product.CategoryId = pro.CategoryId;
                product.SupplierId = pro.SupplierId;

                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                return Ok(new { StatusCode = 201, Message = "Update Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        public async Task<ActionResult<Product>> PostProduct(Product pro)
        {
            try
            {
                var product = new Product();
                {
                    product.Id = pro.Id;
                    product.Img = pro.Img;
                    product.Title = pro.Title;
                    product.Description = pro.Description;
                    product.Active = pro.Active;
                    product.CategoryId = pro.CategoryId;
                    product.SupplierId = pro.SupplierId;
                }
                _context.Products.Add(product);
                await _context.SaveChangesAsync();


                return Ok(new { StatusCode = 201, Message = "Add Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new { StatusCode = 200, Content = "The Product was deleted successfully!!" });
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
