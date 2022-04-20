using KNUElite_project_backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class TaskController : ControllerBase
    {

        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskTaskRepository)
        {
            _taskRepository = taskTaskRepository;
        }

        [HttpGet]
        public IList<JsonResult> Get()
        {
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
        }

        [HttpPost]
        public async Task<IActionResult> Post(Models.Task task)
        { 
            var result = await _taskRepository.Save(task);

            if (!result)
                return BadRequest();
            
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
    }
}
