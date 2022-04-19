using System.Collections.Generic;
using System.Threading.Tasks;
using KNUElite_project_backend.Models;

namespace KNUElite_project_backend.IRepositories
{
    public interface IRoleRepository
    {
        List<Role> Get();
        Task<Role> Get(int id);
        Task<Role> Delete(int id);
        System.Threading.Tasks.Task Add(Role role);
    }
}