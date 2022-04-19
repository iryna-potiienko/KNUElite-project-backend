using System.Collections.Generic;
using System.Threading.Tasks;
using KNUElite_project_backend.Models;
using Task = System.Threading.Tasks.Task;

namespace KNUElite_project_backend.IRepositories
{
    public interface IMeetingRepository
    {
        List<Meeting> Get();
        Task<Meeting> Get(int id);
        Task<Meeting> Delete(int id);
        Task Add(Meeting meeting);
    }
}