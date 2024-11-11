using TaskOrganizer.ViewModel;

namespace TaskOrganizer.Model;

public class DomainModel : BaseViewModel
{
    public int Id { get; set; }
    public bool IsSelected { get; set; }
}
