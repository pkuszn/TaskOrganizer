﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskOrganizer.EFCore
{
    public class TaskContext
    {
        public int Id { get; set; }
        public string TaskDesc { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DoneTaskDate { get; set; }
        public bool IsDoneTask { get; set; }
    }
}
