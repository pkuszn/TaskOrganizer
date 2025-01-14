using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Threading;
using TaskOrganizer.Commands;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Repository.Interfaces;

namespace TaskOrganizer.ViewModels;

public class PomodoroViewModel : BaseViewModel
{
    private int _currentTimeSession;
    private string _currentTimer;
    private string _currentProceededTask;
    private string _amountOfPomodoros;
    private string _pomodoroShortBreak;
    private string _pomodoroLongBreak;
    private string _pomodoroSessionLength;
    private bool _autoStartBreak;
    private bool _autoStartPomodoro;
    private string _longBrakeInterval;
    private string _alarmSound;
    private string _alarmRepeatFrequency;

    private DispatcherTimer PomodoroTimer;
    private readonly ITaskService TaskService;
    public ICommand StartCommand { get; }
    public ICommand StopCommand { get; }
    public ICommand PauseCommand { get; }
    public ICommand ResetCommand { get; }
    public List<Task> Tasks { get; set; }

    #region Session Property
    public string CurrentProceededTask
    {
        get => _currentProceededTask;
        set
        {
            if (_currentProceededTask != value)
            {
                _currentProceededTask = value;
                OnPropertyChanged(nameof(CurrentProceededTask));
            }
        }
    }

    public string CurrentTimer
    { 
        get => _currentTimer;
        set
        {
            if (_currentTimer != value)
            {
                _currentTimer = value;
                OnPropertyChanged(nameof(CurrentTimer));
            }
        }
    }


    #endregion

    #region Pomodoro Settings Property
    public string PomodoroSessions
    {
        get => _amountOfPomodoros;
        set
        {
            if (_amountOfPomodoros != value)
            {
                _amountOfPomodoros = value;
                OnPropertyChanged(nameof(PomodoroSessions));
            }
        }
    }

    public string PomodoroShortBreak
    {
        get => _pomodoroShortBreak;
        set
        {
            if (_pomodoroShortBreak != value)
            {
                _pomodoroShortBreak = value;
                OnPropertyChanged(nameof(PomodoroShortBreak));
            }
        }
    }

    public string PomodoroLongBreak
    {
        get => _pomodoroLongBreak;
        set
        {
            if (_pomodoroLongBreak != value)
            {
                _pomodoroLongBreak = value;
                OnPropertyChanged(nameof(PomodoroLongBreak));
            }
        }
    }

    public string PomodoroSessionLength
    {
        get => _pomodoroSessionLength;
        set
        {
            if (_pomodoroSessionLength != value)
            {
                _pomodoroSessionLength = value;
                OnPropertyChanged(nameof(PomodoroSessionLength));
            }
        }
    }
    #endregion

    #region Break and Alarm Settings Property
    public bool AutoStartBreak
    {
        get => _autoStartBreak;
        set
        {
            if (_autoStartBreak != value)
            {
                _autoStartBreak = value;
                OnPropertyChanged(nameof(AutoStartBreak));
            }
        }
    }

    public bool AutoStartPomodoro
    {
        get => _autoStartPomodoro;
        set
        {
            if (_autoStartPomodoro != value)
            {
                _autoStartPomodoro = value;
                OnPropertyChanged(nameof(AutoStartPomodoro));
            }
        }
    }

    public string LongBrakeInterval
    {
        get => _longBrakeInterval;
        set
        {
            if (_longBrakeInterval != value)
            {
                _longBrakeInterval = value;
                OnPropertyChanged(nameof(LongBrakeInterval));
            }
        }
    }

    public string AlarmSound
    {
        get => _alarmSound;
        set
        {
            if (_alarmSound != value)
            {
                _alarmSound = value;
                OnPropertyChanged(nameof(AlarmSound));
            }
        }
    }

    public string AlarmRepeatFrequency
    {
        get => _alarmRepeatFrequency;
        set
        {
            if (_alarmRepeatFrequency != value)
            {
                _alarmRepeatFrequency = value;
                OnPropertyChanged(nameof(AlarmRepeatFrequency));
            }
        }
    }

    #endregion
    //TODO: sharing data between view models
    public PomodoroViewModel(ITaskService taskService)
    {
        TaskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
        StartCommand = new RelayCommand(StartPomodoroSession);
        StopCommand = new RelayCommand(StopPomodoroSession);
        PauseCommand = new RelayCommand(PausePomodoroSession);
        ResetCommand = new RelayCommand(ResetTimer);
        PomodoroTimer = InitializePomodoroTimer();         //TODO: temporary
        PomodoroSessionLength = "25";  
        _currentTimeSession = 25 * 60;
        CurrentTimer = "25:00";
        BuildTaskAsync();
    }

    private DispatcherTimer InitializePomodoroTimer()
    {
        DispatcherTimer pomodoroTimer = new()
        {
            Interval = TimeSpan.FromSeconds(1)
        };

        pomodoroTimer.Tick += CurrentTimeSessionText;
        return pomodoroTimer;
    }

    private void CurrentTimeSessionText(object sender, EventArgs e)
    {
        if (_currentTimeSession > 0)
        {
            _currentTimeSession--;
        }

        string minutes = (_currentTimeSession / 60).ToString("D2");
        string seconds = (_currentTimeSession % 60).ToString("D2");
        CurrentTimer = $"{minutes}:{seconds}"; 

        if (_currentTimeSession == 0)
        {
            PomodoroTimer.Stop();
        }
    }

    private void ResetTimer(object obj)
    {
        throw new NotImplementedException();
    }

    private void PausePomodoroSession(object obj)
    {
        throw new NotImplementedException();
    }

    private void StopPomodoroSession(object obj)
    {
        throw new NotImplementedException();
    }

    private void StartPomodoroSession(object obj)
    {
        //TODO: temporary
        _currentTimeSession = int.TryParse(PomodoroSessionLength, out int sessionLength) ? sessionLength * 60 : 25 * 60;
        CurrentTimer = "25:00";
        PomodoroTimer.Start();
    }

    private async void BuildTaskAsync()
    {
        Tasks = await TaskService.GetTasksAsync(1);
    }
}
