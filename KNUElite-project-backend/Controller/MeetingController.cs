﻿using KNUElite_project_backend.Models;
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
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingRepository _meetingRepository;

        public MeetingController(IMeetingRepository repository)
        {
            _meetingRepository = repository;
        }

        [HttpGet]
        public IList<Meeting> Get()
        {
            return _meetingRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var meeting = await _meetingRepository.Get(id);
            
            if (meeting == null)
            {
                return NotFound();
            }

            return Ok(meeting);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Meeting meeting)
        {

            await _meetingRepository.Add(meeting);
            return CreatedAtAction("Get", new { id = meeting.Id }, meeting);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var meeting = await _meetingRepository.Delete(id);
            if (meeting == null)
            {
                return NotFound();
            }
            
            return Ok();
        }
    }
}
