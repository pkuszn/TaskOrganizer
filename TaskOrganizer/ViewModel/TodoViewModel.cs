using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskOrganizer.Commands;
using TaskOrganizer.Model;
using TaskOrganizer.Store;

namespace TaskOrganizer.ViewModel;

public class TodoViewModel : BaseViewModel
{
    private string _newTask;
    private static int _id;
    private readonly TodoModel SelectedTask;
    private readonly TodoStore TodoStore;
    private readonly PomodoroStore PomodoroStore;
    private ObservableCollection<TodoModel> TodoList { get; set; } = [];
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
    private readonly ICommand AddNewTaskCommand;
    private readonly ICommand DeleteTaskCommand;
    private readonly ICommand SelectTaskCommand;
    private readonly ICommand DoneTaskCommand;
    public static int ID
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

    public TodoViewModel(TodoStore todoStore, PomodoroStore pomodoroStore)
    {
        TodoStore = todoStore ?? throw new ArgumentNullException(nameof(todoStore));
        PomodoroStore = pomodoroStore ?? throw new ArgumentNullException(nameof(pomodoroStore));
        AddNewTaskCommand = new RelayCommand(AddNewTaskToList);
        DeleteTaskCommand = new RelayCommand(DeleteTaskFromTheList);
        DoneTaskCommand = new RelayCommand(IsTaskSelected);
        UpdateTasks(TodoStore);
        TodoStore.DisplayDoneTasks();
    }

    public bool HasTasksUI => TodoList.Count > 0;

    /// <summary>
    /// Update task on UI
    /// </summary>
    /// <param name="todoStore"></param>
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
        if (NewTask == null || NewTask.Length == 0)
        {
            return;
        }

        ID++;
        TodoModel newTask = new()
        {
            Id = ID,
            Task = NewTask,
            CreatedDate = DateTime.Now,
            DoneTaskDate = DateTime.MinValue,
            IsSelected = false

        };
        TodoStore.AddTask(newTask);
        NewTask = string.Empty;
        UpdateTasks(TodoStore);
    }

    /// <summary>
    /// Delete task from both UI and the store list
    /// </summary>
    private void DeleteTaskFromTheList()
    {
        if (SelectedTask != null)
        {
            foreach (TodoModel item in TodoStore)
            {
                if (item.Id == SelectedTask.Id)
                {
                    TodoStore.DeleteTask(item);
                    TodoList.Remove(SelectedTask);
                    UpdateTasks(TodoStore);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Move done tasks to a new list
    /// </summary>
    private void IsTaskSelected()
    {
        if (SelectedTask != null)
        {
            foreach (TodoModel item in TodoStore)
            {
                if (item.Id == SelectedTask.Id)
                {
                    item.IsSelected = true;
                    item.DoneTaskDate = DateTime.Now;
                    TodoStore.DoneTask(item);
                    TodoStore.DeleteTask(item);
                    TodoList.Remove(SelectedTask);
                    UpdateTasks(TodoStore);
                    break;
                }
            }
            TodoStore.DisplayDoneTasks();
        }
    }
}
