using System;
using System.Windows.Input;
using TaskOrganizer.Store;
using TaskOrganizer.ViewModel;

namespace TaskOrganizer.Helper
{
    public class UpdateViewCommand : ICommand
    {
        private readonly MainViewModel ViewModel;
        private readonly TodoViewModel TodoViewModel;
        private readonly PomodoroViewModel PomodoroViewModel;
        private readonly SettingsViewModel SettingsViewModel;

        private const string TodoViewName = "Todo";
        private const string PomodoroViewName = "Pomodoro";
        private const string SettingsViewName = "Settings";

        public event EventHandler CanExecuteChanged;

        public UpdateViewCommand(MainViewModel viewModel, TodoViewModel todoViewModel, PomodoroViewModel pomodoroViewModel, SettingsViewModel settingsViewModel)
        {
            ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            TodoViewModel = todoViewModel ?? throw new ArgumentNullException(nameof(todoViewModel));
            PomodoroViewModel = pomodoroViewModel ?? throw new ArgumentNullException(nameof(pomodoroViewModel));
            SettingsViewModel = settingsViewModel ?? throw new ArgumentNullException(nameof(settingsViewModel));
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case TodoViewName:
                    ViewModel.BaseViewModel = TodoViewModel;
                    break;
                case PomodoroViewName:
                    ViewModel.BaseViewModel = PomodoroViewModel;
                    break;
                case SettingsViewName:
                    ViewModel.BaseViewModel = SettingsViewModel;
                    break;
            }
        }
    }
}
