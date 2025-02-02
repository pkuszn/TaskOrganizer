using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskOrganizer.Domain.Interfaces;

namespace TaskOrganizer.Domain.Models;
[Table("alarm")]
public class Alarm : IObject<int>
{
    [Key]
    [Required]
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    [Required]
    public string Name { get; set; }
    [Column("path")]
    [Required]
    public string Path { get; set; }
}
