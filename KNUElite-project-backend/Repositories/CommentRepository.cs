using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNUElite_project_backend.IRepositories;
using KNUElite_project_backend.Models;
using Task = System.Threading.Tasks.Task;

//using Task = KNUElite_project_backend.Models.Task;

namespace KNUElite_project_backend.Repositories
{
    public class CommentRepository: ICommentRepository
    {
        private readonly ProjectContex _context;

        public CommentRepository(ProjectContex context)
        {
            _context = context;
        }

        public List<Comment> Get()
        {
            return _context.Comments.ToList();
        }

        public async Task<Comment> Get(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            return comment ?? null;
        }
        
        public async Task<Comment> Delete(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return null;
            }
         
            _context.Comments.Remove(comment);
            await _context.Save();

            return comment;
        }

        public async Task Add(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }
    }
}