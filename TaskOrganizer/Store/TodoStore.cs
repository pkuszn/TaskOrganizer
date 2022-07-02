using AutoMapper;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Domain.Services;
using TaskOrganizer.Model;
namespace TaskOrganizer.Store
{
    /// <summary>
    /// The class responsible for storing data on new tasks
    /// </summary>
    public class TodoStore : IEnumerable<TodoModel>, IEnumerable<TaskModel>
    {
        private readonly IList<TodoModel> todoList;
        private readonly IList<TodoModel> doneTasksList;
        private readonly IList<TaskModel> DTOsTaskList;
        IDataService<TaskModel> _taskService { get; set; }
        IMapper _mapper;
        public TodoStore(IMapper mapper, IDataService<TaskModel> taskService)
        {
            if(mapper is null && taskService is null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }
            _taskService = taskService;
            _mapper = mapper;
            todoList = new List<TodoModel>();
            doneTasksList = new List<TodoModel>();
            DTOsTaskList = new List<TaskModel>();
        }
        public bool HasTasks() => todoList.Count > 0;
        public void AddTask(TodoModel task) => todoList.Add(task);
        public void DeleteTask(TodoModel task) => todoList.Remove(task);
        public async void DoneTask(TodoModel task)
        {
            doneTasksList.Add(task);
            var TaskModel = _mapper.Map<TaskModel>(task);
            DTOsTaskList.Add(TaskModel);
            Debug.WriteLine(TaskModel.IsSelected, TaskModel.TaskDesc);
            await _taskService.Update(6, TaskModel);
        }

        public void InsertMethod()
        {
            var TodoModel = new TodoModel()
            {
                Task = " XDD ", 
                IsSelected = true
            };
            
        }

        public string TopOfTaskList()
        {
            return HasTasks() ? todoList.First().Task.ToString() : "value";
        }
        public IEnumerator<TodoModel> GetEnumerator()
        {
            foreach (TodoModel task in todoList)
            {
                yield return task;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        /// <summary>
        /// TaskModel enumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator<TaskModel> IEnumerable<TaskModel>.GetEnumerator()
        {
            return DTOsTaskList.GetEnumerator();
        }

        public void DisplayDoneTasks()
        {
            foreach(var item in doneTasksList)
            {
                Debug.WriteLine("{0} | {1} | {2} | {3}", item.Id, item.Task, item.CreatedDate, item.DoneTaskDate, item.IsSelected);
            }
        }

        public void TaskRequest()
        {
        }


    }
}
