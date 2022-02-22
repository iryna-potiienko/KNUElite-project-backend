using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNUElite_project_backend.Models
{
    public class Project
    {
        public Project()
        {
            UserProjects = new List<UserProject>();
            Meetings = new List<Meeting>();
            Tasks = new List<Task>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<UserProject> UserProjects { get; set; }
        public List<Meeting> Meetings { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
