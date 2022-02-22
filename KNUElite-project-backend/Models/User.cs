using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNUElite_project_backend.Models
{
    public class User
    {
        public User()
        {
            UserProjects = new List<UserProject>();
            UserMeetings = new List<UserMeeting>();
            ReporterTasks = new List<Task>();
            AssigneeTasks = new List<Task>();
            Comments = new List<Comment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public List<UserProject> UserProjects { get; set; }
        public List<UserMeeting> UserMeetings { get; set; }
        public List<Task> ReporterTasks { get; set; }
        public List<Task> AssigneeTasks { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
