using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleProject.Data;
using SaleProject.Entities.Users;
using SaleProject.Web.Models.Users.Role;

namespace SaleProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly SystemDBContext _context;

        public RolesController(SystemDBContext context)
        {
            _context = context;
        }

        // GET: api/Roles/List
        [HttpGet("[action]")]
        public async Task<IEnumerable<RoleViewModel>> List()
        {
            var rol = await _context.Roles.ToListAsync();

            return rol.Select(r => new RoleViewModel
            {
                idrole = r.idrol,
                name = r.rolname,
                description = r.descriptionrol,
                condition = r.condition
            });
        }

        // GET: api/Roles/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var rol = await _context.Roles.Where(r => r.condition).ToListAsync();

            return rol.Select(r => new SelectViewModel
            {
                idrole = r.idrol,
                name = r.rolname,
            });
        }

        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.idrol == id);
        }
    }
}