using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TaskOrganizer.Helper;
using TaskOrganizer.ViewModel;

namespace TaskOrganizer.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        //Selected view model
        private BaseViewModel _selectedViewModel;

        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand UpdateViewCommand { get; set; }

        public MainViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }
           

        
    }
}
