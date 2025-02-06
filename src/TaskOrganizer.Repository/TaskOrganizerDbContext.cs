using Microsoft.EntityFrameworkCore;
using TaskOrganizer.Domain.Models;

namespace TaskOrganizer.Repository;
public class TaskOrganizerDbContext : DbContext
{
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Pomodoro> Pomodoros { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PomodoroSession> PomodoroSessions { get; set; } 
    public DbSet<Alarm> Alarms { get; set; }
    public DbSet<PomodoroInterval> PomodoroIntervals { get; set; }
    public DbSet<LongBreak> LongBreaks { get; set; }
    public DbSet<ShortBreak> ShortBreaks { get; set; }
    public DbSet<TaskCategory> TaskCategories { get; set; }
    public TaskOrganizerDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}

