using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskOrganizer.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        TodoViewModel _todoViewModel;
        public SettingsViewModel(TodoViewModel todoViewModel = null)
        {
            _todoViewModel = todoViewModel;
        }
    }
}
