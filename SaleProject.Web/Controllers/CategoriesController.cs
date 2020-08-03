using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleProject.Data;
using SaleProject.Entities.Store;
using SaleProject.Web.Models.Stores.Category;

namespace SaleProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly SystemDBContext _context;

        public CategoriesController(SystemDBContext context)
        {
            _context = context;
        }

        // GET: api/Categories/List
        [HttpGet("[action]")]
        public async Task <IEnumerable<CategoryViewModel>> List()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories.Select(category => new CategoryViewModel
            {
                idcategory = category.idcategory,
                name = category.namecategory,
                description = category.categorydescription,
                condition = category.condition
            });
         }

        // GET: api/Categories/Show/5
        [HttpGet("[action]/{id}")]   
        public async Task<IActionResult> Show([FromRoute] int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(new CategoryViewModel
            {
                idcategory = category.idcategory,
                name = category.namecategory,
                description = category.categorydescription,
                condition = category.condition

            });
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory([FromRoute] int id, [FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.idcategory)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.idcategory }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.idcategory == id);
        }
    }
}