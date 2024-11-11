using System;
using TaskOrganizer.Domain.Interfaces;

namespace TaskOrganizer.Domain.Models;

public class Pomodoro : IObject<int>
{
    public int Id { get; set; }
    public bool IsSelected { get; set; }
    public int TimeSpent { get; set; }
    public DateTime Time { get; set; }
    public int TaskId { get; set; }
    public int UserId { get; set; }
}
