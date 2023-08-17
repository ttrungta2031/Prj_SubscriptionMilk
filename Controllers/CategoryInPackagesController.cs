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
    public class CategoryInPackagesController : ControllerBase
    {
        private readonly SubcriptionMilkContext _context;

        public CategoryInPackagesController(SubcriptionMilkContext context)
        {
            _context = context;
        }

        // GET: api/CategoryInPackages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryInPackage>>> GetCategoryInPackages()
        {
            return await _context.CategoryInPackages.ToListAsync();
        }

        // GET: api/CategoryInPackages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryInPackage>> GetCategoryInPackage(int id)
        {
            var categoryInPackage = await _context.CategoryInPackages.FindAsync(id);

            if (categoryInPackage == null)
            {
                return NotFound();
            }

            return categoryInPackage;
        }

        // PUT: api/CategoryInPackages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryInPackage(int id, CategoryInPackage categoryInPackage)
        {
            if (id != categoryInPackage.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(categoryInPackage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryInPackageExists(id))
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

        // POST: api/CategoryInPackages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryInPackage>> PostCategoryInPackage(CategoryInPackage categoryInPackage)
        {
            _context.CategoryInPackages.Add(categoryInPackage);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CategoryInPackageExists(categoryInPackage.CategoryId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCategoryInPackage", new { id = categoryInPackage.CategoryId }, categoryInPackage);
        }

        // DELETE: api/CategoryInPackages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryInPackage(int id)
        {
            var categoryInPackage = await _context.CategoryInPackages.FindAsync(id);
            if (categoryInPackage == null)
            {
                return NotFound();
            }

            _context.CategoryInPackages.Remove(categoryInPackage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryInPackageExists(int id)
        {
            return _context.CategoryInPackages.Any(e => e.CategoryId == id);
        }
    }
}
