using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNUElite_project_backend.IRepositories;
using KNUElite_project_backend.Models;
using Microsoft.AspNetCore.Cors;
using Type = KNUElite_project_backend.Models.Type;

namespace KNUElite_project_backend.Controller
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeRepository _typeRepository;

        public TypeController(ITypeRepository repository)
        {
            _typeRepository = repository;
        }

        [HttpGet]
        public IList<Type> Get()
        {
            return _typeRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var type = await _typeRepository.Get(id);
            
            if (type == null)
            {
                return NotFound();
            }

            return Ok(type);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Type type)
        {
            await _typeRepository.Add(type);
            return CreatedAtAction("Get", new { id = type.Id }, type);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var type = await _typeRepository.Delete(id);
            if (type == null)
            {
                return NotFound();
            }
            
            return Ok();
        }
    }
}
