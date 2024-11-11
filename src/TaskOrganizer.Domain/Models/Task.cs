using System;
using TaskOrganizer.Domain.Interfaces;

namespace TaskOrganizer.Domain.Models;

public class Task : IObject<int>
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? FinishDate { get; set; }
    public bool IsSelected { get; set;  }
    public bool IsDone { get; set; }
}
