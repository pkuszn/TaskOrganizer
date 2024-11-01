using System;

namespace TaskOrganizer.Domain.Models;

public class PomodoroModel : DomainObject
{
    public int TimeSpent { get; set; }
    public DateTime Date { get; set; }
}
