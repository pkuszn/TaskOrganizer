using System;
using System.Windows.Input;

namespace TaskOrganizer.Commands;

public class RelayCommand : ICommand
{
    public event EventHandler CanExecuteChanged;

    private readonly Action Action;

    public RelayCommand(Action action)
    {
        Action = action;
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        Action();
    }
}
