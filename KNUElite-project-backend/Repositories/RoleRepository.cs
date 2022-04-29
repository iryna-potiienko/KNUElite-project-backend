using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNUElite_project_backend.IRepositories;
using KNUElite_project_backend.Models;
using Task = System.Threading.Tasks.Task;

namespace KNUElite_project_backend.Repositories
{
    public class RoleRepository: IRoleRepository
    {
        private readonly ProjectContex _context;

        public RoleRepository(ProjectContex context)
        {
            _context = context;
        }

        public List<Role> Get()
        {
            return _context.Roles.ToList();
        }

        public async Task<Role> Get(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            return role ?? null;
        }
        
        public async Task<Role> Delete(int id)
        {
            var roles = await _context.Roles.FindAsync(id);
            if (roles == null)
            {
                return null;
            }
         
            _context.Roles.Remove(roles);
            await _context.Save();

            return roles;
        }

        public async Task Add(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }
    }
}