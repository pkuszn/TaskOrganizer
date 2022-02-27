using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using TaskOrganizer.Model;

namespace TaskOrganizer.ViewModel
{
    public class TodoViewModel : BaseViewModel
    {
        //Lista zadań do realizacji.
        public ObservableCollection<TodoModel> TodoList { get; set; } = new ObservableCollection<TodoModel>();

        public string NewTask { get; set; }

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
                CreatedDate = DateTime.Now
            };
            TodoList.Add(NewTaskInstantion);
            Debug.WriteLine(TodoList); // Sprawdzenie zawartości listy.
            NewTask = string.Empty;
            OnPropertyChanged(nameof(NewTask));

        }



    }
}
