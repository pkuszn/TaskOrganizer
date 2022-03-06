using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using TaskOrganizer.Model;

namespace TaskOrganizer.ViewModel
{
    public class TodoViewModel : BaseViewModel
    {
        private string _newTask;
        private bool _isCheckedTask;

        public TodoModel SelectedTask { get; set; }

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

        public bool IsCheckedTask
        {
            get
            {
                return _isCheckedTask;
            }
            set
            {
                if (_isCheckedTask != value)
                    _isCheckedTask = value;
                OnPropertyChanged(nameof(IsCheckedTask));
            }
        }

   
        public ICommand AddNewTaskCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }

        public TodoViewModel()
        {
            AddNewTaskCommand = new RelayCommand(AddNewTaskToList);
            DeleteTaskCommand = new RelayCommand(DeleteTaskFromTheList);
        }

        public string ShareTopOfTodoList() => TodoList.First().Task.ToString();

        public bool HasTasks => TodoList.Count > 0;

        private void AddNewTaskToList()
        {
            IsCheckedTask = false;
            if(NewTask == null || NewTask.Length == 0)
            {
                // do nothing
            }
            else
            {
                var NewTaskInstantion = new TodoModel
                {
                    Task = NewTask,
                    CreatedDate = DateTime.Now,
                    IsSeleted = IsCheckedTask
                };
                Debug.WriteLine(NewTaskInstantion);
                TodoList.Add(NewTaskInstantion);
                Debug.WriteLine(TodoList); // Sprawdzenie zawartości listy.
                NewTask = string.Empty;
            }
        }
        private void DeleteTaskFromTheList()
        {
            if(SelectedTask != null)
            {
                TodoList.Remove(SelectedTask);
            }
        }

        private void IsTaskSelected()
        {
            
        }


 
    }
}
