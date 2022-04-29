using KNUElite_project_backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNUElite_project_backend.IRepositories;

namespace KNUElite_project_backend.Controller
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository repository)
        {
            _userRepository = repository;
        }

        [HttpGet]
        public IList<User> Get()
        {
            return _userRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = _userRepository.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            var result = await _userRepository.Add(user);
            if (!result)
                return BadRequest();
            
            return CreatedAtAction("Get", new { id = user.Id }, user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var user = await _userRepository.Delete(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok();
        }
  
        [HttpPost("check")]
        public async Task<IActionResult> CheckUser([FromBody] JObject data)
        {
            var email = data["email"].ToString();
            var password = data["password"].ToString();
            
            var user = _userRepository.CheckUser(email, password);

            if (user == null)
                return BadRequest("Unknown email");

            if (user.Password.Equals(password)) 
            {
                return Ok(new JsonResult(new { Id = user.Id, Name = user.Name, Email = user.Email, Role = user.Role.Name })); 
            }

            return BadRequest("Wrong password");

        }

        [HttpGet("list")]
        public IList<JsonResult> UserList()
        {
            var users = _userRepository.GetList();
            return users;
        }
    }
}
