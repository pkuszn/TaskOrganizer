using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using TaskOrganizer.Commands;
using TaskOrganizer.Helpers;
using TaskOrganizer.Model;
using TaskOrganizer.Store;
using static TaskOrganizer.Helpers.FileHelpers;

namespace TaskOrganizer.ViewModel;

public class PomodoroViewModel : BaseViewModel
{
    private string _currentTime;
    private string _currentDate;
    private int _currentPomodoroTick;
    private TimeSpan _outputTime;
    private string _strTime;
    private string _currentTask;
    private readonly string ImageFilePath = @"C:\Users\patry\source\repos\TaskOrganizer\TaskOrganizer\Icons\tomato.png";
    private readonly string AudioFilePath = @"C:\Users\patry\OneDrive\Pulpit\repos\TaskOrganizer\TaskOrganizer\Audio\audio.wav";
    private readonly string fileName = "\\Audio\\audio.wav";
    private static SoundPlayer player;
    private static int time;
    private string _amountOfPomodoros;
    private readonly TodoStore TodoStore;
    private readonly PomodoroStore PomodoroStore;
    private readonly ICommand StartCountingCommand;
    private readonly ICommand StopCountingCommand;
    private readonly ICommand DebugCountingCommand;
    private readonly ICommand StopCountingEarlyCommand;
    private readonly ICommand ResetCountingCommand;
    private DispatcherTimer Time { get; set; }
    private DispatcherTimer PomodoroTimer { get; set; }
    private ObservableCollection<ImageViewer> ListOfPomodoros { get; set; } = [];
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

    public string StrTime
    {
        get
        {
            return _strTime;
        }
        set
        {
            if (_strTime != value)
                _strTime = value;
            OnPropertyChanged(nameof(StrTime));
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

    public PomodoroViewModel(TodoStore todoStore, PomodoroStore pomodoroStore)
    {
        if (PomodoroTimer != null && Time != null)
        {
            PomodoroTimer.Stop();
            Time.Stop();
            PomodoroTimer = null;
            Time = null;
        }
        PomodoroStore = pomodoroStore;
        TodoStore = todoStore;
        StartCountingCommand = new RelayCommand(CommandCountingSelector);
        StopCountingCommand = new RelayCommand(StopPomodoroTimer);
        DebugCountingCommand = new RelayCommand(DebugTime);
        StopCountingEarlyCommand = new RelayCommand(SumHoursIfClockStopsEarly);
        ResetCountingCommand = new RelayCommand(ResetPomodoroTimer);
        DateText();
        UpdateTime();
        UpdateCurrentTask();
        UpdateAmountOfPomodoros();
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
        if (Time == null)
        {
            Time = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            Time.Tick += new EventHandler(TimeText);
            Time.Start();
        }
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
        PomodoroTimer?.Stop();
    }
    private void ResumePomodoroTimer() => PomodoroTimer.Start();

    private void UpdateCurrentTask()
    {
        CurrentTask = TodoStore.TopOfTaskList();
    }

    private void StartPomodoroTimer()
    {
        if (CurrentPomodoroTick != 0)
        {
            PomodoroModel startNewPomodoroRound = new PomodoroModel()
            {
                Timer = CurrentPomodoroTick
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
    }


    private void ResetPomodoroTimer()
    {
        if (PomodoroTimer != null)
        {
            PomodoroTimer.Stop();
            PomodoroTimer = null;
            StrTime = string.Empty;
        }
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
            StrTime = string.Empty;
            if (File.Exists(AudioFilePath.GetFilePath()))
            {
                PlayAlarmSong(AudioFilePath.GetFilePath());
            }
            else
            {
                throw new IOException("File is unaccessible");
            }
            Debug.WriteLine(CurrentPomodoroTick);
            PomodoroStore.SumHours(PomodoroTimer, CurrentPomodoroTick);
            UpdateAmountOfPomodoros();
            AddNewPomodoroUI();
        }
        StrTime = string.Format("{0:D2}m:{1:D2}s", OutputTime.Minutes, OutputTime.Seconds);
    }

    /// <summary>
    /// Stop a timer and add up the given time - should be improved
    /// </summary>
    private void SumHoursIfClockStopsEarly()
    {
        if (PomodoroTimer != null)
        {
            PomodoroTimer.Stop();
            Debug.WriteLine(time / 60);
            PomodoroStore.SumHours(PomodoroTimer, CurrentPomodoroTick, time);
            PomodoroTimer = null;
            StrTime = string.Empty;
            UpdateAmountOfPomodoros();
        }
    }

    private void UpdateAmountOfPomodoros()
    {
        AmountOfPomodoros = PomodoroStore.GetSummedHours();
    }

    /// <summary>
    /// Adding icons on UI when PomodoroTimer stops and finished correctly
    /// </summary>
    private void AddNewPomodoroUI()
    {
        if (File.Exists(ImageFilePath.GetFilePath()))
        {
            Debug.WriteLine("File exists");
            ImageViewer newInstanceOfPomodoro = new()
            {
                FilePath = ImageFilePath.GetFilePath()
            };
            ListOfPomodoros.Add(newInstanceOfPomodoro);
        }
    }

    private static void PlayAlarmSong(string filePath)
    {
        if (File.Exists(filePath.GetFilePath()))
        {
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
