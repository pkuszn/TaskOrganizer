using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskOrganizer.Domain.Models
{
    public class DomainObject
    {
        public int Id { get; set; }
        public bool IsSelected { get; set; }
    }
}
