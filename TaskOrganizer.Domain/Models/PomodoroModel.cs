using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskOrganizer.Domain.Models
{
    public class PomodoroModel : DomainObject
    {
        public int TimeSpent { get; set; }
        public DateTime Date { get; set; }
    }
}
