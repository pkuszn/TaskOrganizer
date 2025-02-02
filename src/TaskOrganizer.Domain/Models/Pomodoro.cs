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
    [Column("id_pomodoro_session")]
    [Required]
    public int IdPomodoroSession { get; set; }
    [Column("interval")]
    [Required]
    public int Interval { get; set; }
    [Column("created_date")]
    [Required]
    public DateTime CreatedDate { get; set; }
    [Column("finish_date")]
    public DateTime? FinishDate { get; set; }
    [Column("id_user")]
    [Required]
    public int IdUser { get; set; }
    [Column("id_task")]
    [Required]
    public int IdTask { get; set; }
}
