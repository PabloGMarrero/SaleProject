using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleProject.Data;
using SaleProject.Entities.Sales;
using SaleProject.Web.Models.Sales.Person;

namespace SaleProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly SystemDBContext _context;

        public PeopleController(SystemDBContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin, Seller")]
        // GET: api/People/ListClientes
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonViewModel>> ListClientes()
        {
            var people = await _context.People.Where(p => p.typeperson == "Client").ToListAsync();

            return people.Select(p => new PersonViewModel
            {
                idperson = p.idperson,
                name = p.nameperson,
                documenttype = p.documenttype,
                documentnumber = p.numerdocument,
                phone = p.phone,
                email = p.email,
                address = p.addressperson
            });
        }

        // GET: api/People/ListProviders
        [Authorize(Roles = "Admin, Grocerykeeper")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonViewModel>> ListProviders()
        {
            var people = await _context.People.Where(p => p.typeperson == "Provider").ToListAsync();

            return people.Select(p => new PersonViewModel
            {
                idperson = p.idperson,
                name = p.nameperson,
                documenttype = p.documenttype,
                documentnumber = p.numerdocument,
                phone = p.phone,
                email = p.email,
                address = p.addressperson
            });
        }

        // POST: api/Users/Create
        [Authorize(Roles = "Admin, Grocerykeeper, Seller")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (emailAlreadyExist(model.email)) return BadRequest("Email already exist.");

            Person person = new Person
            {
                typeperson = model.typeperson,
                nameperson = model.name,
                documenttype = model.documenttype,
                numerdocument = model.documentnumber,
                addressperson = model.address,
                phone = model.phone,
                email = model.email.ToLower()
            };

            _context.People.Add(person);
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

        private bool emailAlreadyExist(string email)
        {
            return _context.People.AnyAsync(p => p.email == email).Result;
        }

        // PUT: api/Users/Update/
        [Authorize(Roles = "Admin, Grocerykeeper, Seller")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateViewModel model)
        {
            if (!ModelState.IsValid || model.id <= 0) return BadRequest(ModelState);

            var person = await _context.People.FirstOrDefaultAsync(
                p => p.idperson == model.id);

            if (person == null) return NotFound();

            person.typeperson = model.typeperson;
            person.nameperson = model.name;
            person.documenttype = model.documenttype;
            person.numerdocument = model.documentnumber;
            person.email = model.email.ToLower();
            person.addressperson = model.address;
            person.phone = model.phone;

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

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.idperson == id);
        }
    }
}