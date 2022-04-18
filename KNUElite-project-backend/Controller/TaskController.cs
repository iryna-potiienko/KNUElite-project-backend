using KNUElite_project_backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNUElite_project_backend.IControllers;

namespace KNUElite_project_backend.Controller
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        //private readonly IProjectContext _context;

        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskTaskRepository)
        {
            //_context = context;
            _taskRepository = taskTaskRepository;
        }

        [HttpGet]
        public IList<JsonResult> Get()
        {
            // var tasks = _context.Tasks.Include("Status").Include("Type").Include("Project").Include("Reporter")
            //     .Include("Assignee").ToList();
            // List<JsonResult> result = new List<JsonResult>();
            // foreach (var task in tasks)
            // {
            //     var res = (new JsonResult(new { Title = task.Title, Description = task.Description, Type = task.Type.Name,
            //         Status = task.Status.Name, Project = task.Project.Name, Reporter = task.Reporter.Name,
            //         Assignee = task.Assignee.Name
            //     }));
            //     result.Add(res);
            // }
            var result = _taskRepository.Get();
            return (result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = _taskRepository.Get(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
            
            // return Ok(new JsonResult(new
            // {
            //     Id = task.Id,
            //     Title = task.Title,
            //     Description = task.Description,
            //     TypeId = task.TypeId,
            //     Type = task.Type.Name,
            //     StatusId = task.StatusId,
            //     Status = task.Status.Name,
            //     ProjectId = task.ProjectId,
            //     Project = task.Project.Name,
            //     EstimatedTime = task.EstimatedTime,
            //     LoggedTime = task.LoggedTime,
            //     ReporterId = task.ReporterId,
            //     Reporter = task.Reporter.Name,
            //     AssigneeId = task.AssigneeId,
            //     Assignee = task.Assignee.Name
            // }));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Models.Task task)
        { 
            var result = await _taskRepository.Save(task);

            if (!result)
                return BadRequest();
            
            //return Ok(task);
            return CreatedAtAction("Get", new { id = task.Id }, task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskRepository.Delete(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok();
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Models.Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _taskRepository.Edit(id, task);
            if(!result)
                return BadRequest();

            return CreatedAtAction("Get", new { id = task.Id }, task);
        }

        
        /*private ProjectContex _context;

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
                var res = (new JsonResult(new
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    TypeId = task.TypeId,
                    Type = task.Type.Name,
                    StatusId = task.StatusId,
                    Status = task.Status.Name,
                    ProjectId = task.ProjectId,
                    Project = task.Project.Name,
                    EstimatedTime = task.EstimatedTime,
                    LoggedTime = task.LoggedTime,
                    ReporterId = task.ReporterId,
                    Reporter = task.Reporter.Name,
                    AssigneeId = task.AssigneeId,
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

            return Ok(new JsonResult(new
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                TypeId = task.TypeId,
                Type = task.Type.Name,
                StatusId = task.StatusId,
                Status = task.Status.Name,
                ProjectId = task.ProjectId,
                Project = task.Project.Name,
                EstimatedTime = task.EstimatedTime,
                LoggedTime = task.LoggedTime,
                ReporterId = task.ReporterId,
                Reporter = task.Reporter.Name,
                AssigneeId = task.AssigneeId,
                Assignee = task.Assignee.Name
            }));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Models.Task task)
        {
            _context.Tasks.Add(task);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return CreatedAtAction("Get", new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Models.Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Update(task);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

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
        }*/
    }
}
