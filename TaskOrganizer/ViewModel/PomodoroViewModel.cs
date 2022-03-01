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
        public int timeMinutes { get; set; }
      

        public PomodoroViewModel()
        {
            this.Time = 0;
            StartCountingCommand = new RelayCommand(UpdateTime);
        }

        private void UpdateTime()
        {
            var newTime = new PomodoroModel()
            {   
                time = Time
            };
            timeMinutes = Time * 60;
            while(timeMinutes <= 0)
            {
                timeMinutes--;
                OnPropertyChanged(nameof(timeMinutes));
            }
            OnPropertyChanged(nameof(timeMinutes));
        }


    }
}
