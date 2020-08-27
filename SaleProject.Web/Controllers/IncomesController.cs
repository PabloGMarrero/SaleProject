using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleProject.Data;
using SaleProject.Entities.Store;
using SaleProject.Web.Models.Stores.Income;

namespace SaleProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomesController : ControllerBase
    {
        private readonly SystemDBContext _context;

        public IncomesController(SystemDBContext context)
        {
            _context = context;
        }

        // GET: api/Incomes/List
        [Authorize(Roles = "Admin, Grocerykeeper")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<IncomeViewModel>> List()
        {
            var incomes = await _context.Incomes.
                Include(u => u.user).
                Include(p=> p.person).
                OrderByDescending(i=>i.idincome).
                Take(100).
                ToListAsync();

            return incomes.Select(income => new IncomeViewModel
            {
                idincome = income.idincome,
                idsupplier = income.idsupplier,
                nameSupplier = income.person.nameperson,
                iduser = income.iduser,
                nameUsuer = income.user.nameuser,
                vouchertype = income.vouchertype,
                vouchernumber = income.vouchernumber,
                voucherserie = income.voucherserie,
                dateincome = income.dateincome,
                tax = income.tax,
                total = income.total,
                stateincome = income.stateincome
            });
        }

        // POST: api/Incomes/Create
        [Authorize(Roles = "Admin, Grocerykeeper")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var currentDateTime = DateTime.Now;

            Income anIncome = new Income
            {
               idsupplier = model.idsupplier,
               iduser = model.iduser,
               vouchertype = model.vouchertype,
               voucherserie = model.voucherserie,
               vouchernumber =model.vouchernumber,
               dateincome = currentDateTime,
               tax = model.tax,
               total = model.total,
               stateincome = "Accepted"
            };

            _context.Incomes.Add(anIncome);
            try
            {
                await _context.SaveChangesAsync();

                var idIncomeCreated = anIncome.idincome;
                foreach (var detail in model.details)
                {
                    DetailIncome detailIncome = new DetailIncome
                    {
                        idincome = idIncomeCreated,
                        iditem = detail.iditem,
                        amount = detail.amount,
                        price = detail.total
                    };
                    _context.DetailsIncome.Add(detailIncome);
                }

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }


        private bool IncomeExists(int id)
        {
            return _context.Incomes.Any(e => e.idincome == id);
        }
    }
}