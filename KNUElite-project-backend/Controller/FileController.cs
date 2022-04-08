using KNUElite_project_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNUElite_project_backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private ProjectContex _context;

        public FileController(ProjectContex context)
        {
            _context = context;
        }

        [HttpGet]
        public IList<File> Get()
        {
            return (_context.Files.ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var files = await _context.Files.FindAsync(id);

            if (files == null)
            {
                return NotFound();
            }

            return Ok(files);
        }

        [HttpPost]
        public async Task<IActionResult> Post(File files)
        {
            _context.Files.Add(files);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = files.Id }, files);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var files = await _context.Files.FindAsync(id);
            if (files == null)
            {
                return NotFound();
            }

            _context.Files.Remove(files);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
