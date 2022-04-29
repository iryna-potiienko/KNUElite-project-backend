using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNUElite_project_backend.IRepositories;
using KNUElite_project_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace KNUElite_project_backend.Repositories
{
    public class MeetingsRepository: IMeetingRepository
    {
        private readonly ProjectContex _context;

        public MeetingsRepository(ProjectContex context)
        {
            _context = context;
        }

        public List<Meeting> Get()
        {
            return _context.Meetings.ToList();
        }

        public async Task<Meeting> Get(int id)
        {
            var meeting = await _context.Meetings.FindAsync(id);

            return meeting ?? null;
        }
        
        public async Task<Meeting> Delete(int id)
        {
            var meeting = await _context.Meetings.FindAsync(id);
            if (meeting == null)
            {
                return null;
            }
         
            _context.Meetings.Remove(meeting);
            await _context.Save();

            return meeting;
        }

        public async Task Add(Meeting meeting)
        {
            _context.Meetings.Add(meeting);
             await _context.SaveChangesAsync();
        }
    }
}