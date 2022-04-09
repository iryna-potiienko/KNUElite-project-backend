using KNUElite_project_backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNUElite_project_backend.Controller
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ProjectContex _context;

        public TaskController(ProjectContex context)
        {
            _context = context;
        }

        [HttpGet]
        public IList<JsonResult> Get()
        {
            var tasks = _context.Tasks.Include("Status").Include("Type").Include("Project").Include("Reporter")
                .Include("Assignee").ToList();
            List<JsonResult> result = new List<JsonResult>();
            foreach (var task in tasks)
            {
                var res = (new JsonResult(new { Title = task.Title, Description = task.Description, Type = task.Type.Name,
                    Status = task.Status.Name, Project = task.Project.Name, Reporter = task.Reporter.Name,
                    Assignee = task.Assignee.Name
                }));
                result.Add(res);
            }
            return (result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = _context.Tasks.Where(t=>t.Id == id).Include("Status").Include("Type")
                .Include("Project").Include("Reporter")
                .Include("Assignee").FirstOrDefault();

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Models.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = task.Id }, task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
