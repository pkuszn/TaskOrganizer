using System;
using System.Windows.Input;

namespace TaskOrganizer.Commands;

public class RelayCommand : ICommand
{
    public event EventHandler CanExecuteChanged;

    private readonly Action mAction;

    public RelayCommand(Action action)
    {
        mAction = action;
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        mAction();
    }
}
