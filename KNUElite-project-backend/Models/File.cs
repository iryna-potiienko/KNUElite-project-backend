using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNUElite_project_backend.Models
{
    public class File
    {
        public int Id { get; set; }
        public string RootDirectory { get; set; }
        public string FileName { get; set; }
        public DateTime CreationDate { get; set; }
        public long FileSize { get; set; }
        public string FileStream { get; set; }
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
    }
}
