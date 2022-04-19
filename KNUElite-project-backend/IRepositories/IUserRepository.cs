using System.Collections.Generic;
using System.Threading.Tasks;
using KNUElite_project_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace KNUElite_project_backend.IRepositories
{
    public interface IUserRepository
    {
        List<User> Get();
        User Get(int id);
        Task<User> Delete(int id);
        Task<bool> Add(User user);
        List<JsonResult> GetList();
        JsonResult Check(string email, string password);
        User CheckUser(string email, string password);
    }
}