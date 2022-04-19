using System.Collections.Generic;
using System.Threading.Tasks;
using KNUElite_project_backend.Models;

namespace KNUElite_project_backend.IRepositories
{
    public interface ITypeRepository
    {
        List<Type> Get();
        Task<Type> Get(int id);
        Task<Type> Delete(int id);
        System.Threading.Tasks.Task Add(Type type);
    }
}