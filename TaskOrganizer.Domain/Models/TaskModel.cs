using System;

namespace TaskOrganizer.Domain.Models;

public class TaskModel : DomainObject
{
    public string TaskDesc { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime DoneTaskDate { get; set; }
}
