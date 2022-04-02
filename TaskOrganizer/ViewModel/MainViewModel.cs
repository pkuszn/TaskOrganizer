using AutoMapper;
using System.Diagnostics;
using System.Windows.Input;
using TaskOrganizer.Helper;
using TaskOrganizer.Store;
using static TaskOrganizer.MapperProfiles.MyMapper;
namespace TaskOrganizer.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        //Selected view model
        private BaseViewModel _selectedViewModel;
        private TodoStore todoStore { get; set; }
        private PomodoroStore pomodoroStore { get; set; }
        IMapper _mapper { get; set; }
        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                Debug.WriteLine(SelectedViewModel);
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand UpdateViewCommand { get; set; }

        public MainViewModel()
        {
            _mapper = _mapper.Initialize();
            todoStore = new TodoStore(_mapper);
            pomodoroStore = new PomodoroStore(_mapper);
            _selectedViewModel = new TodoViewModel(todoStore, pomodoroStore);
            UpdateViewCommand = new UpdateViewCommand(this, todoStore, pomodoroStore);
        }
           

        
    }
}
