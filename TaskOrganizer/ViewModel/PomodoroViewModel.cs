using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using TaskOrganizer.Helpers;
using TaskOrganizer.Model;
using TaskOrganizer.Store;
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
        private string _currentTask;

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

        public string CurrentTask
        {
            get
            {
                return _currentTask;
            }
            set
            {
                if (_currentTask != value)
                    _currentTask = value;
                OnPropertyChanged(CurrentTask);
            }
        }

        public string AmountOfPomodoros
        {
            get
            {
                return _amountOfPomodoros;
            }
            set
            {
                if (_amountOfPomodoros != value)
                    _amountOfPomodoros = value;
                OnPropertyChanged(nameof(AmountOfPomodoros));
            }
        }

        public readonly string ImageFilePath = @"C:\Users\patry\source\repos\TaskOrganizer\TaskOrganizer\Icons\tomato.png";
        public readonly string AudioFilePath = @"C:\Users\patry\source\repos\TaskOrganizer\TaskOrganizer\Audio\audio.wav";
        public static SoundPlayer player;
        public static int time;
        private string _amountOfPomodoros;

        public DispatcherTimer Time { get; set; }
        public DispatcherTimer PomodoroTimer { get; set; }
        public ICommand StartCountingCommand { get; set; }
        public ICommand StopCountingCommand { get; set; }
        public ICommand DebugCountingCommand { get; set; }
        public ICommand StopCountingEarlyCommand { get; set; }
        public ObservableCollection<ImageViewer> ListOfPomodoros { get; set; } = new ObservableCollection<ImageViewer>(); 
        private TodoStore TodoStore { get; set; }
        private PomodoroStore PomodoroStore { get; set; }


        public PomodoroViewModel(TodoStore todoStore, PomodoroStore pomodoroStore)
        {
            PomodoroStore = pomodoroStore;
            TodoStore = todoStore;
            DateText();
            UpdateTime();
            CurrentTask = TodoStore.TopOfTaskList();
            UpdateAmountOfPomodoros();
            StartCountingCommand = new RelayCommand(CommandCountingSelector);
            StopCountingCommand = new RelayCommand(StopPomodoroTimer);
            DebugCountingCommand = new RelayCommand(DebugTime);
            StopCountingEarlyCommand = new RelayCommand(SumHoursIfClockStopsEarly);
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

        private void StopPomodoroTimer()
        {
            if (PomodoroTimer != null)
                PomodoroTimer.Stop();
        }
        private void ResumePomodoroTimer() => PomodoroTimer.Start();


        private void StartPomodoroTimer()
        {
            var startNewPomodoroRound = new PomodoroModel()
            {
                timer = CurrentPomodoroTick
            };
            time = CurrentPomodoroTick;
            PomodoroTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            time *= 60;
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
            OutputTime = TimeSpan.FromSeconds(time);
            time--;
            OutputTime = OutputTime.Subtract(TimeSpan.FromSeconds(1));
            if (OutputTime.Minutes == 0 && OutputTime.Seconds == 0)
            {
                PomodoroTimer.Stop();
                PomodoroTimer = null;
                strTime = string.Empty;
                PlayAlarmSong(AudioFilePath);
                PomodoroStore.SumHours(CurrentPomodoroTick);
                //AddNewPomodoroUI();
            }
            strTime = string.Format("{0:D2}m:{1:D2}s", OutputTime.Minutes, OutputTime.Seconds);
        }

        private void SumHoursIfClockStopsEarly()
        {
            PomodoroTimer.Stop();
            PomodoroStore.SumHours(time);
            PomodoroTimer = null;
            strTime = string.Empty;
        }

        private void UpdateAmountOfPomodoros()
        {
            if (AmountOfPomodoros != null)
            {
                AmountOfPomodoros = PomodoroStore.AmountOfHours();
            }
        }

        /// <summary>
        /// Adding icons on UI when PomodoroTimer stops and finished correctly
        /// </summary>
        private void AddNewPomodoroUI()
        {
            if (File.Exists(ImageFilePath.getFilePath()))
            {
                Debug.WriteLine("File exists");
                var newInstanceOfPomodoro = new ImageViewer()
                {
                    filePath = ImageFilePath.getFilePath()
                };
                ListOfPomodoros.Add(newInstanceOfPomodoro);
            }
        }

        private static void PlayAlarmSong(string filePath)
        {
            if (File.Exists(filePath.getFilePath())){
                player = new SoundPlayer(filePath);
                player.PlaySync();
                player = null;
            }
        }

        private void DebugTime()
        {
            time = 5;
        }
    }
}
