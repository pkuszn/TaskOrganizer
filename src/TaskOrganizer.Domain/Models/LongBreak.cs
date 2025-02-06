using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TaskOrganizer.Domain.Interfaces;

namespace TaskOrganizer.Domain.Models;
[Table("long_break")]
public class LongBreak : IObject<int>
{
    [Column("id")]
    [Required]
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}