using System.Collections.Generic;
using System.Threading.Tasks;
using KNUElite_project_backend.Models;
using Task = System.Threading.Tasks.Task;

namespace KNUElite_project_backend.IRepositories
{
    public interface IProjectRepository
    {
        List<Project> Get();
        Task<Project> Get(int id);
        Task<Project> Delete(int id);
        Task Add(Project meeting);
    }
}