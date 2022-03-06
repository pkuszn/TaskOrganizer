using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskOrganizer.Model;

namespace TaskOrganizer.Store
{
    public class TodoStore : IEnumerable<TodoModel>
    {
        private IList<TodoModel> todoList;

        public TodoStore()
        {
            todoList = new List<TodoModel>();
        }

        public void AddTask(TodoModel task)
        {
            todoList.Add(task);
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
    }
}
