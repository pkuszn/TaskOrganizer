using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskOrganizer.Domain.Interfaces;

namespace TaskOrganizer.Domain.Models;

[Table("user")]
public class User : IObject<int>
{
    [Column("id")]
    [Required]
    [Key]
    public int Id { get; set; }
    [Column("login")]
    public string Login { get; set; }
    [Column("password")]
    public string Password { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("is_valid")]
    public bool IsValid { get; set; }
}