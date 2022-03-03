using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskOrganizer.Model;

namespace TaskOrganizer.ViewModel
{
    public class TodoViewModel : BaseViewModel
    {
        private string _newTask;

        //Lista zadań do realizacji.
        public ObservableCollection<TodoModel> TodoList { get; set; } = new ObservableCollection<TodoModel>();
        public string NewTask
        {
            get
            {
                return _newTask;
            }
            set
            {
                if (_newTask != value)
                    _newTask = value;
                OnPropertyChanged(nameof(NewTask));
            }
        }
        public ICommand AddNewTaskCommand { get; set; }

        public TodoViewModel()
        {
            AddNewTaskCommand = new RelayCommand(AddNewTaskToList);
        }

        private void AddNewTaskToList()
        {
            var NewTaskInstantion = new TodoModel
            {
                Task = NewTask,
                CreatedDate = DateTime.Now,
                IsSelected = false
            };
            Debug.WriteLine(NewTaskInstantion);
            TodoList.Add(NewTaskInstantion);
            Debug.WriteLine(TodoList); // Sprawdzenie zawartości listy.
            NewTask = string.Empty;
        }
    }
}
