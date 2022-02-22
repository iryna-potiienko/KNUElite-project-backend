using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNUElite_project_backend.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public DateTime Time { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public virtual User User { get; set; }
        public virtual Task Task { get; set; }
    }
}
