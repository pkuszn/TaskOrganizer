using System;

namespace TaskOrganizer.ViewModel;

public class SettingsViewModel : BaseViewModel
{
    TodoViewModel TodoViewModel;
    public SettingsViewModel(TodoViewModel todoViewModel = null)
    {
        TodoViewModel = todoViewModel ?? throw new ArgumentNullException(nameof(todoViewModel));
    }
}
