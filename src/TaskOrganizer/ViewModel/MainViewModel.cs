using AutoMapper;
using System;
using System.Diagnostics;
using System.Windows.Input;
namespace TaskOrganizer.ViewModel;

public class MainViewModel : BaseViewModel
{
    private BaseViewModel SelectedViewModel;
    private readonly IMapper Mapper;

    public ICommand UpdateViewCommand { get; set; }
    public BaseViewModel BaseViewModel
    {
        get { return SelectedViewModel; }
        set
        {
            SelectedViewModel = value;
            Debug.WriteLine(BaseViewModel);
            OnPropertyChanged(nameof(BaseViewModel));
        }
    }

    public MainViewModel(TodoViewModel todoViewModel, IMapper mapper)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        SelectedViewModel = todoViewModel ?? throw new ArgumentNullException(nameof(todoViewModel));
    }
}
