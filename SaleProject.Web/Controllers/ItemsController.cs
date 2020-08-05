using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleProject.Data;
using SaleProject.Entities.Store;
using SaleProject.Web.Models.Stores.Item;

namespace SaleProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly SystemDBContext _context;

        public ItemsController(SystemDBContext context)
        {
            _context = context;
        }

        // GET: api/Items/List
        [HttpGet("[action]")]
        public async Task<IEnumerable<ItemViewModel>> List()
        {
            var items = await _context.Items.Include(cat => cat.category).ToListAsync();
            return items.Select(item => new ItemViewModel
            {
                iditem = item.iditem,
                idcategory = item.idcategory,
                namecategory = item.category.namecategory,
                code = item.code,
                nameitem = item.nameitem,
                price = item.price,
                stock = item.stock,
                itemdescription = item.itemdescription,
                condition = item.condition
            });
        }

        // GET: api/Items/Show/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Show([FromRoute] int id)
        {
            var item = await _context.Items.Include(a => a.category)
                .SingleOrDefaultAsync(a => a.iditem == id);

            if (item == null) return NotFound();

            return Ok(new ItemViewModel
            {
                iditem = item.iditem,
                idcategory = item.idcategory,
                namecategory = item.category.namecategory,
                code = item.code,
                nameitem = item.nameitem,
                price = item.price,
                stock = item.stock,
                itemdescription = item.itemdescription,
                condition = item.condition

            });
        }

        // PUT: api/Items/Update
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateViewModel model)
        {
            if (!ModelState.IsValid || model.idcategory <= 0) return BadRequest(ModelState);

            var item = await _context.Items.FirstOrDefaultAsync(
                c => c.iditem == model.iditem);

            if (item == null) return NotFound();

            item.idcategory = model.idcategory; 
            item.code = model.code;
            item.nameitem = model.nameitem;
            item.price = model.price;
            item.stock = model.stock;
            item.itemdescription = model.itemdescription;

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

        // POST: api/Items/Create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Item anItem = new Item
            {
                idcategory = model.idcategory,
                code = model.code,
                nameitem = model.nameitem,
                price = model.price,
                stock = model.stock,
                condition = true
            };

            _context.Items.Add(anItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        // Change condition: api/Items/ChangeCondition/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> ChangeCondition([FromRoute] int id)
        {
            if (id <= 0) return BadRequest(ModelState);

            var item = await _context.Items.FirstOrDefaultAsync(c => c.iditem == id);

            if (item == null) return NotFound();

            item.condition = !item.condition;

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

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.iditem == id);
        }
    }
}