using System;
using System.Windows.Input;
using Serilog;

namespace TaskOrganizer.Commands;
public class UpdateViewCommand : ICommand
{
    private readonly Action<string> Action;
    public event EventHandler CanExecuteChanged;
    public UpdateViewCommand(Action<string> action)
    {
        Action = action ?? throw new ArgumentNullException(nameof(action));   
    }

    public bool CanExecute(object parameter) => true;

    public void Execute(object parameter)
    {
        if (parameter is string viewName)
        {
            Log.Information($"You chose: {viewName}", typeof(UpdateViewCommand));
            Action(viewName);
        } 
    }
}
