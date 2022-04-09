using KNUElite_project_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNUElite_project_backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ProjectContex _context;

        public UserController(ProjectContex context)
        {
            _context = context;
        }

        [HttpGet]
        public IList<User> Get()
        {
            return (_context.Users.ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = _context.Users.Where(t=>t.Id == id).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = user.Id }, user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("check")]
        public async Task<IActionResult> CheckUser([FromBody] JObject data)
        {
            var email = data["email"].ToString();
            var password = data["password"].ToString();
            var user = _context.Users.Where(t => t.Email.Equals(email)).Include("Role").FirstOrDefault();

            if (user == null)
                return BadRequest("Unknown email");

            if (user.Password.Equals(password)) 
            {
                return Ok(new JsonResult(new { Id = user.Id, Name = user.Name, Email = user.Email, Role = user.Role.Name })); 
            }

            return BadRequest("Wrong password");

        }
    }
}
