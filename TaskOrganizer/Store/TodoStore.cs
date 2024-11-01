using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Domain.Services;
using TaskOrganizer.Model;
namespace TaskOrganizer.Store;

/// <summary>
/// The class responsible for storing data on new tasks
/// </summary>
public class TodoStore : IEnumerable<TodoModel>, IEnumerable<TaskModel>
{
    private readonly IList<TodoModel> TodoList;
    private readonly IList<TodoModel> DoneTasksList;
    private readonly IList<TaskModel> DTOsTaskList;
    private readonly IDataService<TaskModel> TaskService;
    private readonly IMapper Mapper;
    public TodoStore(IMapper mapper, IDataService<TaskModel> taskService)
    {
        TaskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        TodoList = [];
        DoneTasksList = [];
        DTOsTaskList = [];
    }
    public bool HasTasks()
    {
        return TodoList.Count > 0;
    }

    public void AddTask(TodoModel task)
    {
        TodoList.Add(task);
    }

    public void DeleteTask(TodoModel task)
    {
        TodoList.Remove(task);
    }

    public async void DoneTask(TodoModel task)
    {
        DoneTasksList.Add(task);
        TaskModel TaskModel = Mapper.Map<TaskModel>(task);
        DTOsTaskList.Add(TaskModel);
        Debug.WriteLine(TaskModel.IsSelected, TaskModel.TaskDesc);
        await TaskService.Update(6, TaskModel);
    }

    public string TopOfTaskList()
    {
        return HasTasks() ? TodoList.First().Task.ToString() : "value";
    }

    public IEnumerator<TodoModel> GetEnumerator()
    {
        foreach (TodoModel task in TodoList)
        {
            yield return task;
        }
    }

    public void DisplayDoneTasks()
    {
        foreach (TodoModel item in DoneTasksList)
        {
            Debug.WriteLine("{0} | {1} | {2} | {3}", item.Id, item.Task, item.CreatedDate, item.DoneTaskDate, item.IsSelected);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator<TaskModel> IEnumerable<TaskModel>.GetEnumerator()
    {
        return DTOsTaskList.GetEnumerator();
    }
}
