using System;

namespace TaskOrganizer.Model;
public class TaskModel : DomainModel
{
    public string Task { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime DoneTaskDate { get; set; }
}
