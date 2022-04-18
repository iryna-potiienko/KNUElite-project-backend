using System.Collections.Generic;
using System.Threading.Tasks;
using KNUElite_project_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace KNUElite_project_backend.IControllers
{
    public interface IUserRepository
    {
        List<User> Get();
        User Get(int id);
        Task<User> Delete(int id);
        Task<bool> Add(User user);
        JsonResult Check(string email, string password);
        User Check1(string email, string password);
    }
}