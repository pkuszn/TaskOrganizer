using System.ComponentModel;

namespace TaskOrganizer.ViewModel;

/// <summary>
/// Interaction logic for BaseViewModel
/// </summary>
public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

    protected void OnPropertyChanged(string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
