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

        // PUT: api/Categories/Update
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateViewModel model)
        {
            if (!ModelState.IsValid || model.idcategory <= 0) return BadRequest(ModelState);

            var category = await _context.Categories.FirstOrDefaultAsync(
                c => c.idcategory == model.idcategory);

            if (category == null) return NotFound();

            category.namecategory = model.namecategory ;
            category.categorydescription = model.categorydescription;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }

        // POST: api/Categories/Create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Category aCategory = new Category
            {
                namecategory = model.namecategory,
                categorydescription = model.categorydescription,
                condition = true
            };

            _context.Categories.Add(aCategory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
                
            return Ok();
        }

        // DELETE: api/Categories/Delete/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            _context.Categories.Remove(category);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }

            return Ok(category);
        }

        // Change condition: api/Categories/ChangeCondition/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> ChangeCondition([FromRoute] int id)
        {
            if (id <= 0) return BadRequest(ModelState);

            var category = await _context.Categories.FirstOrDefaultAsync(
                c => c.idcategory == id);

            if (category == null) return NotFound();

            category.condition = !category.condition;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }

        // GET: api/Categories/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var categories = await _context.Categories.Where(c=>c.condition).ToListAsync();
            return categories.Select(category => new SelectViewModel
            {
                idcategory = category.idcategory,
                name = category.namecategory,
            });
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.idcategory == id);
        }
    }
}