using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskOrganizer.Domain.Interfaces;

namespace TaskOrganizer.Domain.Models;

[Table("pomodoro")]
public class Pomodoro : IObject<int>
{
    [Column("id")]
    [Required]
    [Key]
    public int Id { get; set; }
    [Column("time_spent")]
    public int TimeSpent { get; set; }
    [Column("time")]
    public DateTime Time { get; set; }
    [Column("task_id")]
    public int TaskId { get; set; }
    [Column("user_id")]
    public int UserId { get; set; }
}
