using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskOrganizer.Commands;
using TaskOrganizer.Model;

namespace TaskOrganizer.ViewModels;

public class TaskViewModel : BaseViewModel
{
    private string _newTask;
    private static int _id;
    private readonly TaskModel SelectedTask;
    private ObservableCollection<TaskModel> TodoList { get; set; } = [];
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

    public TaskViewModel()
    {
        AddNewTaskCommand = new RelayCommand(AddNewTaskToList);
        DeleteTaskCommand = new RelayCommand(DeleteTaskFromTheList);
        DoneTaskCommand = new RelayCommand(IsTaskSelected);
        //UpdateTasks(TodoStore);
        //TodoStore.DisplayDoneTasks();
    }

    public bool HasTasksUI => TodoList.Count > 0;

    /// <summary>
    /// Update task on UI
    /// </summary>
    /// <param name="todoStore"></param>
    private void UpdateTasks()
    {
        //if (todoStore.HasTasks())
        //{
        //    TodoList.Clear();
        //    foreach (var item in todoStore)
        //    {
        //        TodoList.Add(item);
        //    }
        //}
    }

    private void AddNewTaskToList(object obj)
    {
        if (NewTask == null || NewTask.Length == 0)
        {
            return;
        }

        ID++;
        TaskModel newTask = new()
        {
            Id = ID,
            Task = NewTask,
            CreatedDate = DateTime.Now,
            DoneTaskDate = DateTime.MinValue,
            IsSelected = false

        };
        //TodoStore.AddTask(newTask);
        NewTask = string.Empty;
        //UpdateTasks(TodoStore);
    }

    /// <summary>
    /// Delete task from both UI and the store list
    /// </summary>
    private void DeleteTaskFromTheList(object obj)
    {
        //if (SelectedTask != null)
        //{
        //    foreach (TodoModel item in TodoStore)
        //    {
        //        if (item.Id == SelectedTask.Id)
        //        {
        //            TodoStore.DeleteTask(item);
        //            TodoList.Remove(SelectedTask);
        //            UpdateTasks(TodoStore);
        //            break;
        //        }
        //    }
        //}
    }

    /// <summary>
    /// Move done tasks to a new list
    /// </summary>
    private void IsTaskSelected(object obj)
    {
        //if (SelectedTask != null)
        //{
        //    foreach (TodoModel item in TodoStore)
        //    {
        //        if (item.Id == SelectedTask.Id)
        //        {
        //            item.IsSelected = true;
        //            item.DoneTaskDate = DateTime.Now;
        //            TodoStore.DoneTask(item);
        //            TodoStore.DeleteTask(item);
        //            TodoList.Remove(SelectedTask);
        //            UpdateTasks(TodoStore);
        //            break;
        //        }
        //    }
        //    TodoStore.DisplayDoneTasks();
        //}
    }
}
