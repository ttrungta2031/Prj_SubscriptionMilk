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
    public class CategoriesController : ControllerBase
    {
        private readonly SubcriptionMilkContext _context;
        private readonly IGetallService _service;
        public CategoriesController(SubcriptionMilkContext context, IGetallService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/Categories
        [HttpGet("Getallcategories")]
        public IActionResult GetAllList(string search)
        {
            try
            {
                var result = _service.GetAllCate(search);
                var totalitems = result.Count();

                return Ok(new { StatusCode = 200, Message = "Load successful", data = result, totalitems });
            }
            catch (Exception e)
            {

                return StatusCode(409, new { StatusCode = 409, Message = e.Message });

            }
        }

        // GET: api/Categories/5
        [HttpGet("getbyid")]
        public async Task<ActionResult> Getcategory(int id)
        {
            var all = _context.Categories.AsQueryable();

            all = _context.Categories.Where(us => us.Id.Equals(id));        
            var result = all.ToList();

            return Ok(new { StatusCode = 200, Message = "Load successful", data = result });
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        public async Task<IActionResult> PutCategory(Category cate)
        {

            try
            {
                var category = await _context.Categories.FindAsync(cate.Id);
                if (category == null)
                {
                    return NotFound();
                }
                category.Img = cate.Img;
                category.Title = cate.Title;
                category.Description = cate.Description;
                category.Active = cate.Active;


                _context.Categories.Update(category);
                await _context.SaveChangesAsync();

                return Ok(new { StatusCode = 201, Message = "Update Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        public async Task<ActionResult<Category>> PostCategory(Category cate)
        {
            try
            {
                var category = new Category();
                {
                    category.Id = cate.Id;
                    category.Img = cate.Img;
                    category.Title = cate.Title;
                    category.Description = cate.Description;
                    category.Active = cate.Active;
                }
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();


                return Ok(new { StatusCode = 201, Message = "Add Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }


        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(new { StatusCode = 200, Content = "The Category was deleted successfully!!" });
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
