using System;
using TaskOrganizer.ViewModel;

namespace TaskOrganizer.Model
{
    public class TodoModel : BaseViewModel
    {
        public int TaskID { get; set; }
        public string Task { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DoneTaskDate { get; set; }
        public bool IsSelected { get; set; }

    }
}
