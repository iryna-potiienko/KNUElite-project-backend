using System.Collections.Generic;
using System.Threading.Tasks;
using KNUElite_project_backend.Models;
using Task = System.Threading.Tasks.Task;

namespace KNUElite_project_backend.IRepositories
{
    public interface ICommentRepository
    {
        List<Comment> Get();
        Task<Comment> Get(int id);
        Task<Comment> Delete(int id);
        Task Add(Comment comment);
    }
}