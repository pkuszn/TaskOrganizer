using System;
using TaskOrganizer.ViewModel;

namespace TaskOrganizer.Model
{
    public class TodoModel : DomainModel
    {
        public string Task { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DoneTaskDate { get; set; }
        public bool IsSelected { get; set; }

    }
}
