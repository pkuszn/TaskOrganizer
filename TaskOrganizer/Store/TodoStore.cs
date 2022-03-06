using System;
using System.Collections.Generic;
using System.Text;
using TaskOrganizer.Model;

namespace TaskOrganizer.Store
{
    public class TodoStore
    {
        private readonly List<TodoModel> _todo;

        public IEnumerable<TodoModel> Tasks => _todo;
        public TodoStore()
        {
            _todo = new List<TodoModel>();
        }
    }
}
