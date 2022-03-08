using System;
using System.Collections;
using System.Collections.Generic;
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
        private IList<TodoModel> todoList;
        private IList<TodoModel> doneTasksList;
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
            return HasTasks() ? todoList.First().Task.ToString() : "";
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

        public void displayDoneTasks()
        {
            foreach(var item in doneTasksList)
            {
                Console.WriteLine("{0} | {1} | {2} | {3}", item.TaskID, item.Task, item.CreatedDate, item.IsSelected);
            }
        }
    }
}
