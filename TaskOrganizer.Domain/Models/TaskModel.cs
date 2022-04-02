using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskOrganizer.Domain.Models
{
    public class TaskModel : DomainObject
    {
        public string TaskDesc { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DoneTaskDate { get; set; }
        public bool IsSelected { get; set; }
    }
}
