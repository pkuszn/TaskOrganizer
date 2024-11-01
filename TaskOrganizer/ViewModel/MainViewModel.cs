using AutoMapper;
using System.Diagnostics;
using System.Windows.Input;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Domain.Services;
using TaskOrganizer.Helper;
using TaskOrganizer.Store;
using static TaskOrganizer.MapperProfiles.MyMapper;
namespace TaskOrganizer.ViewModel;

public class MainViewModel : BaseViewModel
{
    private BaseViewModel SelectedViewModel;
    private readonly TodoStore TodoStore;
    private readonly PomodoroStore PomodoroStore;
    private readonly IMapper Mapper;
    private readonly IDataService<TaskModel> TaskService;
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

    public MainViewModel()
    {
        Mapper = Mapper.Initialize();
        TodoStore = new TodoStore(Mapper, TaskService);
        PomodoroStore = new PomodoroStore(Mapper);
        SelectedViewModel = new TodoViewModel(TodoStore, PomodoroStore);
        UpdateViewCommand = new UpdateViewCommand(this, TodoStore, PomodoroStore);
    }
}
