using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNUElite_project_backend.Models
{
    public class Meeting
    {
        public Meeting()
        {
            UserMeetings = new List<UserMeeting>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public List<UserMeeting> UserMeetings { get; set; }

    }
}
