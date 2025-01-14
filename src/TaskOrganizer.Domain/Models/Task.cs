using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TaskOrganizer.Domain.Interfaces;

namespace TaskOrganizer.Domain.Models;

[Table("task")]
public class Task : IObject<int>
{
    [Column("id")]
    [Required]
    [Key]
    public int Id { get; set; }
    [Column("name")]
    [Required]
    public string Name { get; set; }
    [Column("id_user")]
    [Required]
    public int IdUser { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("created_date")]
    public DateTime CreatedDate { get; set; }
    [Column("finish_date")]
    public DateTime? FinishDate { get; set; }
    [Column("is_done")]
    public bool IsDone { get; set; }
}
