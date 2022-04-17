using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNUElite_project_backend.IControllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = KNUElite_project_backend.Models.Task;

namespace KNUElite_project_backend.Repositories
{
    public class TaskRepository: ITaskRepository
    {
        private readonly ProjectContex _context;

        public TaskRepository(ProjectContex context)
        {
            _context = context;
        }

        public IList<JsonResult> Get()
        {
            var tasks = _context.Tasks.Include("Status").Include("Type").Include("Project").Include("Reporter")
                .Include("Assignee").ToList();
            List<JsonResult> result = new List<JsonResult>();
            foreach (var task in tasks)
            {
                var res = (new JsonResult(new { Id = task.Id, Title = task.Title, Description = task.Description, Type = task.Type.Name,
                    Status = task.Status.Name, Project = task.Project.Name, Reporter = task.Reporter.Name,
                    Assignee = task.Assignee.Name
                }));
                result.Add(res);
            }
            return (result);
        }

        public Task Get(int id)
        {
            var task = _context.Tasks.Where(t=>t.Id == id).Include("Status").Include("Type")
                .Include("Project").Include("Reporter")
                .Include("Assignee").First();

            if (task == null)
            {
                return null;
            }

            return task;
        }

        public async Task<Task> Delete(int id)
         {
             var task = await _context.Tasks.FindAsync(id);
             if (task == null)
             {
                 return null;
             }
         
             _context.Tasks.Remove(task);
             await _context.Save();

             return task;
         }

         public void Save(Task task)
         {
             _context.Tasks.Add(task);
             _context.Save();
         }
    }
}