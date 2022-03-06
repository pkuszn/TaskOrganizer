using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using TaskOrganizer.Model;
using TaskOrganizer.Store;

namespace TaskOrganizer.ViewModel
{
    public class TodoViewModel : BaseViewModel
    {
        private string _newTask;
        private bool _isCheckedTask;

        public TodoModel SelectedTask { get; set; }

        //Lista zadań do realizacji.
        public ObservableCollection<TodoModel> TodoList { get; set; } = new ObservableCollection<TodoModel>();
        private TodoStore TodoStore { get; set; }
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

        public TodoViewModel(TodoStore todoStore = null)
        {
            //Update TodoList
            this.TodoStore = todoStore;
            if(todoStore == null)
            {
                todoStore = new TodoStore();
            }
            UpdateTasks(todoStore);
            AddNewTaskCommand = new RelayCommand(AddNewTaskToList);
            DeleteTaskCommand = new RelayCommand(DeleteTaskFromTheList);
        }

        public string ShareTopOfTodoList() => TodoList.First().Task.ToString();

        public bool HasTasks => TodoList.Count > 0;

        private void UpdateTasks(TodoStore todostore)
        {
            // Czyścimy Widokowa todo liste i aktualizujemy ją za każdym razem gdy wchodzimy do widoku Todo
            //Przy tworzeniu nowego Taska, musimy zawrzeć todoListe w pamięci, która będzię aktywna przez cały czas i w każdym miejscu w pamięci
            TodoList.Clear();
            foreach (var item in todostore)
            {
                TodoList.Add(item);
            }
        }

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
                TodoStore.AddTask(NewTaskInstantion);
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
