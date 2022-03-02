using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Threading;
using TaskOrganizer.ViewModel;

namespace TaskOrganizer.Model
{
    public class PomodoroModel : BaseViewModel
    {
        //Time property for pomodoro counter
        public DispatcherTimer timer { get; set; }
    }
}
