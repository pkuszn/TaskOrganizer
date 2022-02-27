using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TaskOrganizer.ViewModel
{
    public class PomodoroViewModel : BaseViewModel
    {
        public ICommand StartCountingCommand { get; set; }
    }
}
