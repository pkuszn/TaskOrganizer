using TaskOrganizer.Domain.Interfaces;

namespace TaskOrganizer.Domain.Models;

public class User : IObject<int>
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public bool IsSelected { get; set;  }
}