using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskOrganizer.Domain.Interfaces;

namespace TaskOrganizer.Domain.Models;
[Table("pomodoro_interval")]
public class PomodoroInterval : IObject<int>
{
    [Column("id")]
    [Required]
    [Key]
    public int Id { get; set;  }
    [Required]
    public string Name { get; set; }
}
