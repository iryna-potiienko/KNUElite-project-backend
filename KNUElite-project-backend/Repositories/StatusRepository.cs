using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNUElite_project_backend.IRepositories;
using KNUElite_project_backend.Models;
using Task = System.Threading.Tasks.Task;

namespace KNUElite_project_backend.Repositories
{
    public class StatusRepository: IStatusRepository
    {
        private readonly ProjectContex _context;

        public StatusRepository(ProjectContex context)
        {
            _context = context;
        }

        public List<Status> Get()
        {
            return _context.Statuses.ToList();
        }

        public async Task<Status> Get(int id)
        {
            var status = await _context.Statuses.FindAsync(id);

            return status ?? null;
        }
        
        public async Task<Status> Delete(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
            {
                return null;
            }
         
            _context.Statuses.Remove(status);
            await _context.Save();

            return status;
        }

        public async Task Add(Status status)
        {
            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();
        }
    }
}