﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SaleProject.Data;
using SaleProject.Entities.Users;
using SaleProject.Web.Models.Users.User;

namespace SaleProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SystemDBContext _context;
        private readonly IConfiguration _config;

        public UsersController(SystemDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/Users/List
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<UserViewModel>> List()
        {
            var users = await _context.Users.Include(u=> u.role).ToListAsync();

            return users.Select(u => new UserViewModel
            {
                iduser = u.iduserperson,
                idrole = u.idrol,
                name = u.nameuser,
                role = u.role.rolname,
                documentType = u.documenttype,
                documentNumber = u.numerdocument,
                phone = u.phone,
                email = u.email,
                address = u.addressuser,
                password = u.passwordhash,
                condition = u.condition
            }) ;
        }

        // PUT: api/Users/Update/
        [Authorize(Roles = "Admin")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateViewModel model )
        {
            if (!ModelState.IsValid || model.iduser <= 0) return BadRequest(ModelState);

            var user = await _context.Users.FirstOrDefaultAsync(
                u => u.iduserperson == model.iduser);

            if (user == null) return NotFound();

            user.idrol = model.idrole;
            user.nameuser = model.name;
            user.documenttype = model.documenttype;
            user.numerdocument = model.documentnumber; 
            user.email = model.email.ToLower();
            user.addressuser = model.address;
            user.phone = model.phone;

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

        // PUT: api/Users/ChangePassword/
        [HttpPut("[action]")]
        public async Task<IActionResult> ChangePassword([FromBody] UpdatePassViewModel model)
        {
            if (!ModelState.IsValid || model.iduser <= 0) return BadRequest(ModelState);

            var user = await _context.Users.FirstOrDefaultAsync(
                u => u.iduserperson == model.iduser);

            if (user == null) return NotFound();

            EncryptPassword(model.password, out byte[] passHash, out byte[] passSalt);
            user.passwordhash = passHash;
            user.passwordsalt = passSalt;

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

        // POST: api/Users/Create
        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (emailAlreadyExist(model.email)) return BadRequest("Email already exist.");

            EncryptPassword(model.password, out byte[] passHash, out byte[] passSalt);

            User user = new User
            {
                idrol = model.idrole,
                nameuser = model.name,
                documenttype = model.documenttype,
                numerdocument = model.documentnumber,
                addressuser = model.address,
                phone = model.phone,
                email = model.email.ToLower(),
                passwordhash = passHash,
                passwordsalt = passSalt,
                condition = true
            };

            _context.Users.Add(user);
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

        private bool emailAlreadyExist(string email)
        {
            return _context.Users.AnyAsync(u => u.email == email).Result;
        }

        private void EncryptPassword(string pass, out byte[] passHash, out byte[] passSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512() )
            {
                passSalt = hmac.Key;
                passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pass));
            }
        }

        // Change condition: api/Users/ChangeCondition/5
        [Authorize(Roles = "Admin")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> ChangeCondition([FromRoute] int id)
        {
            if (id <= 0) return BadRequest(ModelState);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.iduserperson == id);

            if (user == null) return NotFound();

            user.condition = !user.condition;

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

        // POST: api/Users/Login
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model )
        {
            var email = model.email.ToLower();
            if (!emailAlreadyExist(model.email)) return NotFound("Email not exists.");

            var user = await _context.Users.
                Where(u=>u.condition).
                Include(u => u.role).
                FirstOrDefaultAsync(u => u.email == model.email);

            if (user == null) return NotFound("User does not exist. Please register first.");

            if (!VerifyPassword(model.password, user.passwordhash, user.passwordsalt))
            {
                NotFound("Email or password does not match. Please try again.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.iduserperson.ToString()),
                new Claim(ClaimTypes.Email, user.email.ToLower()),
                new Claim(ClaimTypes.Role, user.role.rolname),
                new Claim("idusuario", user.iduserperson.ToString()),
                new Claim("rol", user.role.rolname),
                new Claim("nombre", user.nameuser)
            };

            return Ok(
                new { token = GenerateToken(claims) }
                );
        }

        private bool VerifyPassword(string password, byte[] passHashStored, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(passHashStored).SequenceEqual(new ReadOnlySpan<byte>(passHash));
            }
        }

        private string GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds,
                claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.iduserperson == id);
        }
    }
}