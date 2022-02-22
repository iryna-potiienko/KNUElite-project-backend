using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNUElite_project_backend.Models
{
    public class Type
    {
        public Type()
        {
            Tasks = new List<Task>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Task> Tasks { get; set; }

    }
}
