using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNUElite_project_backend.IRepositories;
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

            var result = ConvertToJsonList(tasks);
            return (result);
        }

        public JsonResult Get(int id)
        {
            var task = _context.Tasks.Where(t=>t.Id == id).Include("Status").Include("Type")
                .Include("Project").Include("Reporter")
                .Include("Assignee").FirstOrDefault();;

            if (task == null)
            {
                return null;
            }

            return ConvertToJsonResult(task);
        }

        public async Task<Task> Delete(int id)
         {
             var task = await _context.Tasks.FindAsync(id);
             if (task == null)
             {
                 return null;
             }

            var comments = _context.Comments.Where(t => t.TaskId == task.Id).ToList();
            foreach(var comment in comments)
            {
                _context.Comments.Remove(comment);
            }

            await _context.Save();

            _context.Tasks.Remove(task);
             await _context.Save();

             return task;
         }

         public async Task<bool> Save(Task task)
         {
             _context.Tasks.Add(task);
             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (Exception e)
             {
                 return false;
             }

             return true;
         }
         public async Task<bool> Edit(int id, Models.Task task)
         {
             _context.Update(task);

             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (Exception e)
             {
                 return false;
             }

             return true;
         }

         public JsonResult ConvertToJsonResult(Task task)
         {
             var result = new JsonResult(new
             {
                 Id = task.Id,
                 Title = task.Title,
                 Description = task.Description,
                 TypeId = task.TypeId,
                 Type = task.Type.Name,
                 StatusId = task.StatusId,
                 Status = task.Status.Name,
                 ProjectId = task.ProjectId,
                 Project = task.Project.Name,
                 EstimatedTime = task.EstimatedTime,
                 LoggedTime = task.LoggedTime,
                 ReporterId = task.ReporterId,
                 Reporter = task.Reporter.Name,
                 AssigneeId = task.AssigneeId,
                 Assignee = task.Assignee.Name
             });
             
             return result;
         }

         public List<JsonResult> ConvertToJsonList(List<Task> tasks)
         {
             List<JsonResult> result = new List<JsonResult>();
             foreach (var task in tasks)
                 result.Add(ConvertToJsonResult(task));

             return result;
         }
    }
}