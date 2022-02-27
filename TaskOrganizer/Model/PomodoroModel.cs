using System;
using System.Collections.Generic;
using System.Text;
using TaskOrganizer.ViewModel;

namespace TaskOrganizer.Model
{
    public class PomodoroModel : BaseViewModel
    {
        //Time property for pomodoro counter
        public DateTime time { get; set; }
    }
}
