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
    public class StatusController : ControllerBase
    {
        private readonly IStatusRepository _statusRepository;

        public StatusController(IStatusRepository repository)
        {
            _statusRepository = repository;
        }

        [HttpGet]
        public IList<Status> Get()
        {
            return _statusRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var status = await _statusRepository.Get(id);
            
            if (status == null)
            {
                return NotFound();
            }

            return Ok(status);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Status status)
        {
            await _statusRepository.Add(status);
            return CreatedAtAction("Get", new { id = status.Id }, status);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _statusRepository.Delete(id);
            if (status == null)
            {
                return NotFound();
            }
            
            return Ok();
        }
    }
}
