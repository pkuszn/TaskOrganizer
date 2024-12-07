using System;
using System.Windows.Input;

namespace TaskOrganizer.Commands;
public class RelayCommand : ICommand
{
    private readonly Action<object> Action;
    private readonly Predicate<object> CanExecuteCommand;
    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public RelayCommand(Action<object> action)
    {
        Action = action;
        CanExecuteCommand = null;
    }

    public RelayCommand(Action<object> action, Predicate<object> canExecuteCommand)
    {
        Action = action;
        CanExecuteCommand = canExecuteCommand;
    }

    public bool CanExecute(object parameter)
    {
        return CanExecuteCommand == null || CanExecuteCommand(parameter);
    }

    public void Execute(object parameter)
    {
        Action(parameter);
    }
}
