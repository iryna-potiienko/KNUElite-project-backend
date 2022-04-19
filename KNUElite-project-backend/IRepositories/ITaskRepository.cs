using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task = KNUElite_project_backend.Models.Task;

namespace KNUElite_project_backend.IRepositories
{
    public interface ITaskRepository
    { 
        IList<JsonResult> Get();
        JsonResult Get(int id);

        Task<Models.Task> Delete(int id);

        Task<bool> Save(Task task);
        Task<bool> Edit(int id, Task task);
    }
}