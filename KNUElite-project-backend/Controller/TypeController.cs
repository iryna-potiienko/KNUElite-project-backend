using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNUElite_project_backend.Models;

namespace KNUElite_project_backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private ProjectContex _context;

        public TypeController(ProjectContex context)
        {
            _context = context;
        }

        [HttpGet]
        public IList<Models.Type> Get()
        {
            return (_context.Types.ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var type = await _context.Types.FindAsync(id);

            if (type == null)
            {
                return NotFound();
            }

            return Ok(type);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Models.Type type)
        {
            _context.Types.Add(type);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = type.Id }, type);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var type = await _context.Types.FindAsync(id);
            if (type == null)
            {
                return NotFound();
            }

            _context.Types.Remove(type);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
