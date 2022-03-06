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
        public UpdateViewCommand(MainViewModel viewModel, TodoStore todoStore = null)
        {
            this.viewModel = viewModel;
            this.todoStore = todoStore;
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
                    viewModel.SelectedViewModel = new TodoViewModel(todoStore);
                    break;
                case "Pomodoro":
                    viewModel.SelectedViewModel = new PomodoroViewModel();
                    break;
                case "Settings":
                    viewModel.SelectedViewModel = new SettingsViewModel();
                    break;
            }
        }
    }
}
