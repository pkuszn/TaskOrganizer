using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOrganizer.Model;

namespace TaskOrganizer.Store
{
    /// <summary>
    /// The class responsible for storing data on new tasks
    /// </summary>
    public class TodoStore : IEnumerable<TodoModel>
    {
        private readonly IList<TodoModel> todoList;
        private readonly IList<TodoModel> doneTasksList;
        public TodoStore()
        {
            todoList = new List<TodoModel>();
            doneTasksList = new List<TodoModel>();
        }
        public bool HasTasks() => todoList.Count > 0;
        public void AddTask(TodoModel task) => todoList.Add(task);
        public void DeleteTask(TodoModel task) => todoList.Remove(task);
        public void DoneTask(TodoModel task) => doneTasksList.Add(task);
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

        public void DisplayDoneTasks()
        {
            foreach(var item in doneTasksList)
            {
                Debug.WriteLine("{0} | {1} | {2} | {3}", item.TaskID, item.Task, item.CreatedDate, item.DoneTaskDate, item.IsSelected);
            }
        }

        public void TaskRequest()
        {

            //_mapper.Map<TodoModel>(TaskModel)
        }

    }
}
