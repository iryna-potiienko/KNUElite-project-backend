using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNUElite_project_backend.Models
{
    public class Task
    {
        public Task()
        {
            Comments = new List<Comment>();
            Files = new List<File>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public string LoggedTime { get; set; }
        public string EstimatedTime { get; set; }
        public int AssigneeId { get; set; }
        public int ReporterId { get; set; }
        public int ProjectId { get; set; }
        public virtual Type Type { get; set; }
        public virtual Status Status { get; set; }
        public virtual User Assignee { get; set; }
        public virtual User Reporter { get; set; }
        public virtual Project Project { get; set; }
        public List<Comment> Comments { get; set; }
        public List<File> Files { get; set; }

    }
}
