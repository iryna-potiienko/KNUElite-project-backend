using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNUElite_project_backend.IRepositories;
using KNUElite_project_backend.Models;
using Task = System.Threading.Tasks.Task;

namespace KNUElite_project_backend.Repositories
{
    public class TypeRepository: ITypeRepository
    {
        private readonly ProjectContex _context;

        public TypeRepository(ProjectContex context)
        {
            _context = context;
        }

        public List<Type> Get()
        {
            return _context.Types.ToList();
        }

        public async Task<Type> Get(int id)
        {
            var type = await _context.Types.FindAsync(id);

            return type ?? null;
        }
        
        public async Task<Type> Delete(int id)
        {
            var type = await _context.Types.FindAsync(id);
            if (type == null)
            {
                return null;
            }
         
            _context.Types.Remove(type);
            await _context.Save();

            return type;
        }

        public async Task Add(Type type)
        {
            _context.Types.Add(type);
            await _context.SaveChangesAsync();
        }
    }
}