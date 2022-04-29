using KNUElite_project_backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository repository)
        {
            _projectRepository = repository;
        }

        [HttpGet]
        public IList<Project> Get()
        {
            return _projectRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var project = await _projectRepository.Get(id);
            
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Project project)
        {
            await _projectRepository.Add(project);
            return CreatedAtAction("Get", new { id = project.Id }, project);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _projectRepository.Delete(id);
            if (project == null)
            {
                return NotFound();
            }
            
            return Ok();
        }
    }
}
