using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;
using TaskOrganizer.Model;

namespace TaskOrganizer.ViewModel
{
    public class PomodoroViewModel : BaseViewModel
    {
        private string _currentTime;
        private string _currentDate;

        public int InputTime { get; set; }
        public ICommand StartCountingCommand { get; set; }
        
        public DispatcherTimer Time { get; set; }
        public string outputMinutes { get; set; }
      

        public PomodoroViewModel()
        {
            DateText();
            StartCountingCommand = new RelayCommand(UpdateTime);
        }

        public string CurrentTime
        {
            get
            {
                return _currentTime;
            }
            set
            {
                if (_currentTime != value)
                    _currentTime = value;
                OnPropertyChanged(nameof(CurrentTime));
            }
        }

        public string CurrentDate
        {
            get
            {
                return _currentDate;
            }
            set
            {
                if (_currentDate != value)
                    _currentDate = value;
                OnPropertyChanged(nameof(_currentDate));
            }
        }

        private void UpdateTime()
        {

            Time = new DispatcherTimer();
            Time.Interval = TimeSpan.FromSeconds(1);
            Time.Tick += new EventHandler(TimeText);
            Time.Start();
        }

        private void TimeText(object sdender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToString("HH:mm");
        }

        private void DateText()
        {
            CurrentDate = DateTime.Now.ToString("dddd dd MMMM yyyy");
        }
    }
}
