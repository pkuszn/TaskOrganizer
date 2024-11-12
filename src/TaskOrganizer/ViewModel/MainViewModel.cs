using Serilog;
using System;
using System.Windows.Input;
using TaskOrganizer.Commands;

namespace TaskOrganizer.ViewModel;
public class MainViewModel : BaseViewModel
{
    private BaseViewModel SelectedViewModel;
    private const string TodoViewName = "Todo";
    private const string PomodoroViewName = "Pomodoro";
    private const string SettingsViewName = "Settings";
    public ICommand UpdateViewCommand { get; }
    public BaseViewModel BaseViewModel
    {
        get { return SelectedViewModel; }
        set
        {
            SelectedViewModel = value;
            Log.Information($"Selected view: {SelectedViewModel.GetType()}", typeof(BaseViewModel));
            OnPropertyChanged(nameof(BaseViewModel));
        }
    }

    public MainViewModel(TodoViewModel todoViewModel, PomodoroViewModel pomodoroViewModel, SettingsViewModel settingsViewModel)
    {
        BaseViewModel = todoViewModel ?? throw new ArgumentNullException(nameof(todoViewModel));
        UpdateViewCommand = new UpdateViewCommand(viewName =>
        {
            BaseViewModel = viewName switch
            {
                TodoViewName => todoViewModel,
                PomodoroViewName => pomodoroViewModel,
                SettingsViewName => settingsViewModel,
                _ => BaseViewModel
            };
        });
    }
}
