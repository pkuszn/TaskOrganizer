using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TaskOrganizer.Model;

namespace TaskOrganizer.ViewModel
{
    public class PomodoroViewModel : BaseViewModel
    {
        public ICommand StartCountingCommand { get; set; }
        
        public int Time { get; set; }
      

        public PomodoroViewModel()
        {
            this.Time = 0;
            StartCountingCommand = new RelayCommand(UpdateTime);
        }

        private void UpdateTime()
        {
            Random rnd = new Random();
            Time = rnd.Next();
            var newTime = new PomodoroModel()
            {   
                time = Time
            };
            OnPropertyChanged(nameof(Time));
        }
    }
}
