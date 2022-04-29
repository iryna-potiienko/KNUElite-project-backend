using System.Collections.Generic;
using System.Threading.Tasks;
using KNUElite_project_backend.Models;

namespace KNUElite_project_backend.IRepositories
{
    public interface IStatusRepository
    {
        List<Status> Get();
        Task<Status> Get(int id);
        Task<Status> Delete(int id);
        System.Threading.Tasks.Task Add(Status status);
    }
}