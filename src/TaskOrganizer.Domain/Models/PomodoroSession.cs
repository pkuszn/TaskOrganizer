using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskOrganizer.Domain.Interfaces;

namespace TaskOrganizer.Domain.Models;
[Table("pomodoro_session")]
public class PomodoroSession : IObject<int>
{
    [Column("id")]
    [Required]
    [Key]
    public int Id { get; set; }
    [Column("id_user")]
    [Required]
    public int IdUser { get; set; }
    [Column("created_date")]
    [Required]
    public DateTime CreatedDate { get; set; }
    [Column("finish_date")]
    public DateTime FinishDate { get; set; }
    [Column("pomodoro_interval")]
    [Required]
    public int PomodoroInterval { get; set; }
    [Column("short_break")]
    [Required]
    public int ShortBreak { get; set; }
    [Column("long_break")]
    [Required]
    public int LongBreak { get; set; }
    [Column("auto_restart_break")]
    [Required]
    public bool AutoRestartBreak { get; set; }
    [Column("auto_restart_pomodoro")]
    [Required]
    public bool AutoRestartPomodoro { get; set; }
    [Column("id_alarm_sound")]
    [Required]
    public int IdAlarmSound { get; set; }
    [Column("repeat")]
    [Required]
    public bool Repeat { get; set; }
}
