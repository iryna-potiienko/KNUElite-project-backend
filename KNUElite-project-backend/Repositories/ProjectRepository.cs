using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNUElite_project_backend.IRepositories;
using KNUElite_project_backend.Models;
using Task = System.Threading.Tasks.Task;

namespace KNUElite_project_backend.Repositories
{
    public class ProjectRepository: IProjectRepository
    {
        private readonly ProjectContex _context;

        public ProjectRepository(ProjectContex context)
        {
            _context = context;
        }

        public List<Project> Get()
        {
            return _context.Projects.ToList();
        }

        public async Task<Project> Get(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            return project ?? null;
        }
        
        public async Task<Project> Delete(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return null;
            }
         
            _context.Projects.Remove(project);
            await _context.Save();

            return project;
        }

        public async Task Add(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }
    }
}