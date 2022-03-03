using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using System.Windows.Threading;
using TaskOrganizer.Helpers;
using TaskOrganizer.Model;
using static TaskOrganizer.Helpers.PomodoroViewModelExtensions;

namespace TaskOrganizer.ViewModel
{
    public class PomodoroViewModel : BaseViewModel
    {
        private string _currentTime;
        private string _currentDate;
        private int _currentPomodoroTick;
        private TimeSpan _outputTime;
        private string _strTime;

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
                OnPropertyChanged(nameof(CurrentDate));
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
            }
        }

        public TimeSpan OutputTime
        {
            get
            {
                return _outputTime;
            }
            set
            {
                if (_outputTime != value)
                    _outputTime = value;
                OnPropertyChanged(nameof(OutputTime));
            }
        }

        public string strTime
        {
            get
            {
                return _strTime;
            }
            set
            {
                if (_strTime != value)
                    _strTime = value;
                OnPropertyChanged(nameof(strTime));
            }
        }

        public string ImageFilePath = @"C:\Users\patry\source\repos\TaskOrganizer\TaskOrganizer\Icons\tomato.png";

        public DispatcherTimer Time { get; set; }
        public DispatcherTimer PomodoroTimer { get; set; }
        public ICommand StartCountingCommand { get; set; }
        public ICommand StopCountingCommand { get; set; }
        public ObservableCollection<ImageViewer> ListOfPomodoros { get; set; } = new ObservableCollection<ImageViewer>();

        public PomodoroViewModel()
        {
            DateText();
            UpdateTime();
            StartCountingCommand = new RelayCommand(CommandCountingSelector);
            StopCountingCommand = new RelayCommand(StopPomodoroTimer);
        }

        private void CommandCountingSelector()
        {
            if (PomodoroTimer == null)
            {
                StartPomodoroTimer();
            }
            else
            {
                ResumePomodoroTimer();
            }
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

        private void TimeText(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToString("HH:mm:ss");
        }

        private void DateText()
        {
            CurrentDate = DateTime.Now.ToString("dddd dd MMMM yyyy");
        }

        private void StopPomodoroTimer() => PomodoroTimer.Stop();
        private void ResumePomodoroTimer() => PomodoroTimer.Start();


        private void StartPomodoroTimer()
        {
            PomodoroTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            CurrentPomodoroTick *= 60;
            PomodoroTimer.Tick += (s, e) => Task.Run(() => PomodoroTick(s, e));
            PomodoroTimer.Start();
        }

        /// <summary>
        /// Pomodoro tick function - should be improved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PomodoroTick(object sender, EventArgs e)
        {
            OutputTime = TimeSpan.FromSeconds(CurrentPomodoroTick);
            CurrentPomodoroTick--;
            OutputTime = OutputTime.Subtract(TimeSpan.FromSeconds(1));
            if (OutputTime.Minutes == 0 && OutputTime.Seconds == 0)
            {
                PomodoroTimer.Stop();
                PomodoroTimer = null;
                strTime = string.Empty;
                AddNewPomodoroUI();
            }
            strTime = string.Format("{0:D2}m:{1:D2}s", OutputTime.Minutes, OutputTime.Seconds);
        }
        /// <summary>
        /// Adding icons on UI when PomodoroTimer stops and finished correctly
        /// </summary>
        private void AddNewPomodoroUI()
        {
            if (File.Exists(ImageFilePath.getFile()))
            {
                Debug.WriteLine("File exists");
                var newInstancesOfPomodoro = new ImageViewer()
                {
                    filePath = ImageFilePath.getFile()
                };
                OnPropertyChanged(nameof(ListOfPomodoros));
            }
        }
    }
}
