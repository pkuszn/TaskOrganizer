﻿using Serilog;
using System;
using System.Windows.Input;
using TaskOrganizer.Commands;

namespace TaskOrganizer.ViewModels;
public class MainViewModel : BaseViewModel
{
    private BaseViewModel SelectedViewModel;
    private const string TodoViewName = "Task";
    private const string PomodoroViewName = "Pomodoro";
    private const string SettingsViewName = "Settings";
    private const string LoginViewName = "Login";
    private const string UserAccountViewName = "UserAccount";
    private const string AnalyticsViewName = "Analytics";
    public ICommand UpdateViewCommand { get; }
    public BaseViewModel BaseViewModel
    {
        get => SelectedViewModel;
        set
        {
            SelectedViewModel = value;
            Log.Information($"Selected view: {SelectedViewModel.GetType()}", typeof(BaseViewModel));
            OnPropertyChanged(nameof(BaseViewModel));
        }
    }

    public MainViewModel(
        TaskViewModel taskViewModel, 
        PomodoroViewModel pomodoroViewModel, 
        SettingsViewModel settingsViewModel,
        LoginViewModel loginViewModel,
        UserAccountViewModel userAccountViewModel,
        AnalyticsViewModel analyticsViewModel)
    {
        BaseViewModel = taskViewModel ?? throw new ArgumentNullException(nameof(taskViewModel));
        UpdateViewCommand = new UpdateViewCommand(viewName =>
        {
            SwitchView(taskViewModel, pomodoroViewModel, settingsViewModel, loginViewModel, userAccountViewModel, analyticsViewModel, viewName);
        });
    }

    private void SwitchView(
        TaskViewModel taskViewModel, 
        PomodoroViewModel pomodoroViewModel, 
        SettingsViewModel settingsViewModel, 
        LoginViewModel loginViewModel, 
        UserAccountViewModel userAccountViewModel, 
        AnalyticsViewModel analyticsViewModel,
        string viewName)
    {
        BaseViewModel = viewName switch
        {
            TodoViewName => taskViewModel,
            PomodoroViewName => pomodoroViewModel,
            SettingsViewName => settingsViewModel,
            LoginViewName => loginViewModel,
            UserAccountViewName => userAccountViewModel,
            AnalyticsViewName => analyticsViewModel,
            _ => BaseViewModel
        };
    }
}
