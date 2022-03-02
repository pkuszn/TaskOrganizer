using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using System.Windows.Threading;
using TaskOrganizer.Model;

namespace TaskOrganizer.ViewModel
{
    public class PomodoroViewModel : BaseViewModel
    {
        private string _currentTime;
        private string _currentDate;
        private int _currentPomodoroTick;

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

        public int CurrentPomodoroTick
        {
            get
            {
                return _currentPomodoroTick;
            }
            set
            {
                if (_currentPomodoroTick != value)
                    _currentPomodoroTick = value;
                OnPropertyChanged(nameof(_currentPomodoroTick));
            }
        }


        public DispatcherTimer Time { get; set; }
        public DispatcherTimer PomodoroTimer { get; set; }
        public ICommand StartCountingCommand { get; set; }




        public PomodoroViewModel()
        {
            DateText();
            UpdateTime();
            StartCountingCommand = new RelayCommand(StartPomodoroTimer);
        }

        private void UpdateTime()
        {

            Time = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            Time.Tick += new EventHandler(TimeText);
            Time.Start();
        }
    

    private void TimeText(object sdender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToString("HH:mm:ss");
        }

        private void DateText()
        {
            CurrentDate = DateTime.Now.ToString("dddd dd MMMM yyyy");
        }

        private void StartPomodoroTimer()
        {
            PomodoroTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            PomodoroTimer.Tick += (s, e) => Task.Run(() => PomodoroTick(s, e));
            PomodoroTimer.Start();
        }

        private void PomodoroTick(object sdender, EventArgs e)
        {
            CurrentPomodoroTick -= 1;
            if (CurrentPomodoroTick == 0)
                PomodoroTimer.Stop();
            OnPropertyChanged(nameof(CurrentPomodoroTick));
        }
 
    }
   
}
