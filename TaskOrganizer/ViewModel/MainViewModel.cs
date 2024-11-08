using AutoMapper;
using System;
using System.Diagnostics;
using System.Windows.Input;
using TaskOrganizer.Store;
using static TaskOrganizer.MapperProfiles.MyMapper;
namespace TaskOrganizer.ViewModel;

public class MainViewModel : BaseViewModel
{
    private BaseViewModel SelectedViewModel;
    private readonly TodoStore TodoStore;
    private readonly PomodoroStore PomodoroStore;
    private readonly IMapper Mapper;

    public ICommand UpdateViewCommand { get; set; }
    public BaseViewModel BaseViewModel
    {
        get { return SelectedViewModel; }
        set
        {
            SelectedViewModel = value;
            Debug.WriteLine(BaseViewModel);
            OnPropertyChanged(nameof(BaseViewModel));
        }
    }

    public MainViewModel(PomodoroStore pomodoroStore, TodoStore todoStore, TodoViewModel todoViewModel)
    {
        Mapper = Mapper.Initialize();
        TodoStore = todoStore ?? throw new ArgumentNullException(nameof(todoStore));
        PomodoroStore = pomodoroStore ?? throw new ArgumentNullException(nameof(pomodoroStore));
        SelectedViewModel = todoViewModel ?? throw new ArgumentNullException(nameof(todoViewModel));
    }
}
