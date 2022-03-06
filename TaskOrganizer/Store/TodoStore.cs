using System;
using System.Collections;
using System.Collections.Generic;
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

        public TodoStore()
        {
            todoList = new List<TodoModel>();
        }

        public bool HasTasks() => todoList.Count > 0;
        public void AddTask(TodoModel task) => todoList.Add(task);
        public void DeleteTask(TodoModel task) => todoList.Remove(task);

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
    }
}
