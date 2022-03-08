using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using TaskOrganizer.Helper;
using TaskOrganizer.Store;
using TaskOrganizer.ViewModel;

namespace TaskOrganizer.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        //Selected view model
        private BaseViewModel _selectedViewModel;
        private TodoStore todoStore { get; set; }  = new TodoStore();
        private PomodoroStore pomodoroStore { get; set; } = new PomodoroStore();

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
            _selectedViewModel = new TodoViewModel(todoStore, pomodoroStore);
            UpdateViewCommand = new UpdateViewCommand(this, todoStore, pomodoroStore);
        }
           

        
    }
}
