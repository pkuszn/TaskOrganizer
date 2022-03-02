using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TaskOrganizer.ViewModel;

namespace TaskOrganizer.Helper
{
    public class UpdateViewCommand : ICommand
    {
        private MainViewModel viewModel;

        public UpdateViewCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
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
                    viewModel.SelectedViewModel = new TodoViewModel();
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
