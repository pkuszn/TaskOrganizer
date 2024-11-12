using System.ComponentModel;
using Serilog;

namespace TaskOrganizer.ViewModel;

/// <summary>
/// Interaction logic for BaseViewModel
/// </summary>
public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

    protected void OnPropertyChanged(string name = null)
    {
        Log.Information($"Property changed: {name}", typeof(BaseViewModel));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
