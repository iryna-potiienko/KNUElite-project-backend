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
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository repository)
        {
            _commentRepository = repository;
        }

        [HttpGet]
        public IList<Comment> Get()
        {
            return _commentRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var comment = await _commentRepository.Get(id);
            
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Comment comment)
        {
            await _commentRepository.Add(comment);
            return CreatedAtAction("Get", new { id = comment.Id }, comment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _commentRepository.Delete(id);
            if (comment == null)
            {
                return NotFound();
            }
            
            return Ok();
        }
    }
}
