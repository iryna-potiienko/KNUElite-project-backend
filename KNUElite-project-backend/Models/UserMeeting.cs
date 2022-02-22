using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNUElite_project_backend.Models
{
    public class UserMeeting
    {
        public int Id { get; set; }
        public int MeetingId { get; set; }
        public int UserId { get; set; }
        public virtual Meeting Meeting { get; set; }
        public virtual User User { get; set; }
    }
}
