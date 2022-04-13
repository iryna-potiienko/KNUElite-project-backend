using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task = KNUElite_project_backend.Models.Task;

namespace KNUElite_project_backend.IControllers
{
    public interface ITaskRepository
    { 
        IList<JsonResult> Get();
        Models.Task Get(int id);

        Task<Models.Task> Delete(int id);

        void Save(Models.Task task);
    }
}