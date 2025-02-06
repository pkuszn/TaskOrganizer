using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TaskOrganizer.Domain.Interfaces;

namespace TaskOrganizer.Domain.Models;
[Table("short_break")]
public class ShortBreak : IObject<int>
{
    [Column("id")]
    [Required]
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}
