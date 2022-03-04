using System;
using System.Collections.Generic;
using System.Text;
using TaskOrganizer.ViewModel;

namespace TaskOrganizer.Model
{
    public class TodoModel : BaseViewModel
    {
        public string Task { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsSeleted { get; set; }
    }
}
