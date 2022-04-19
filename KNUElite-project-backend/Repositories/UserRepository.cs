using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNUElite_project_backend.IRepositories;
using KNUElite_project_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace KNUElite_project_backend.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly ProjectContex _context;

        public UserRepository(ProjectContex context)
        {
            _context = context;
        }

        public List<User> Get()
        {
            return _context.Users.ToList();
        }

        public User Get(int id)
        {
            var user = _context.Users.FirstOrDefault(t => t.Id == id);

            return user ?? null;
        }

        public List<JsonResult> GetList()
        {
            var users = _context.Users;

            if (users == null)
            {
                return null;
            }

            List<JsonResult> result = new List<JsonResult>();
            foreach (var user in users)
                result.Add(ConvertToJsonResult(user));

            return result;
        }
        public async Task<User> Delete(int id)
         {
             var user = await _context.Users.FindAsync(id);
             if (user == null)
             {
                 return null;
             }
         
             _context.Users.Remove(user);
             await _context.Save();

             return user;
         }

         public async Task<bool> Add(User user)
         {
             _context.Users.Add(user);
             
             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (Exception e)
             {
                 return false;
             }

             return true;
         }
         
         public JsonResult Check(string email, string password)
         {
             var user = _context.Users.Where(t => t.Email.Equals(email)).Include("Role").FirstOrDefault();

             if (user == null)
                 throw new Exception("Unknown email");

             if (user.Password.Equals(password)) 
             {
                 return new JsonResult(new { Id = user.Id, Name = user.Name, Email = user.Email, Role = user.Role.Name }); 
             }

             throw new Exception("Wrong password");
         }
         
         public User CheckUser(string email, string password)
         {
             var user = _context.Users.Where(u => u.Email.Equals(email)).Include("Role").FirstOrDefault();
             return user;
         }

        private JsonResult ConvertToJsonResult(User user)
        {
            var result = new JsonResult(new
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            });

            return result;
        }
    }
}