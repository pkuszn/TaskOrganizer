using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TaskOrganizer.Store;
using TaskOrganizer.ViewModel;

namespace TaskOrganizer.Helper
{
    public class UpdateViewCommand : ICommand
    {
        private MainViewModel viewModel;
        private TodoStore todoStore { get; set; }
        private PomodoroStore pomodoroStore { get; set; }
        public UpdateViewCommand(MainViewModel viewModel, TodoStore todoStore = null, PomodoroStore pomodoroStore = null)
        {
            this.viewModel = viewModel;
            this.todoStore = todoStore;
            this.pomodoroStore = pomodoroStore;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Todo":
                    viewModel.SelectedViewModel = new TodoViewModel(todoStore, pomodoroStore);
                    break;
                case "Pomodoro":
                    viewModel.SelectedViewModel = new PomodoroViewModel(todoStore, pomodoroStore);
                    break;
                case "Settings":
                    viewModel.SelectedViewModel = new SettingsViewModel();
                    break;
            }
        }
    }
}
