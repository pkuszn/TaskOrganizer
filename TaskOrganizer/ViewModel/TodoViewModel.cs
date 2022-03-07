using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using TaskOrganizer.Model;
using TaskOrganizer.Store;

namespace TaskOrganizer.ViewModel
{
    public class TodoViewModel : BaseViewModel
    {
        private string _newTask;
        private static uint _id;

        public TodoModel SelectedTask { get; set; }
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
        public ICommand AddNewTaskCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }
        public ICommand SelectTaskCommand { get; set; }
        public static uint ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                    _id = value;
            }
        }

        public TodoViewModel(TodoStore todoStore = null)
        {
            this.TodoStore = todoStore;
            if(todoStore == null)
                TodoStore = new TodoStore();

            UpdateTasks(TodoStore);
            AddNewTaskCommand = new RelayCommand(AddNewTaskToList);
            DeleteTaskCommand = new RelayCommand(DeleteTaskFromTheList);
            SelectTaskCommand = new RelayCommand(IsTaskSelected);
        }

        public bool HasTasks => TodoList.Count > 0;

        private void UpdateTasks(TodoStore todoStore)
        {
            if (todoStore.HasTasks())
            {
                TodoList.Clear();
                foreach (var item in todoStore)
                {

                    TodoList.Add(item);
                }
            }
        }

        private void AddNewTaskToList()
        {
            if(NewTask == null || NewTask.Length == 0)
            {
                // do nothing
            }
            else
            {
                ID++;
                var NewTaskInstantion = new TodoModel
                {
                    TaskID = ID,
                    Task = NewTask,
                    CreatedDate = DateTime.Now,
                    IsSeleted = false

                };
                TodoStore.AddTask(NewTaskInstantion);
                NewTask = string.Empty;
                UpdateTasks(TodoStore);
            }
        }
        /// <summary>
        /// Delete task from both UI and the store list
        /// </summary>
        private void DeleteTaskFromTheList()
        {
            if(SelectedTask != null)
            {
                foreach(var item in TodoStore)
                {
                    if(item.TaskID == SelectedTask.TaskID)
                    {
                        TodoStore.DeleteTask(item);
                        TodoList.Remove(SelectedTask);
                        UpdateTasks(TodoStore);
                        break;
                    }
                }
            }
        }

        private void IsTaskSelected()
        {
           foreach(var item in TodoList)
            {
                if (item.IsSeleted == true)
                    Debug.WriteLine("clicked");
            }
        }


 
    }
}
