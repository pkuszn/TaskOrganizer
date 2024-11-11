using AutoMapper;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Model;

namespace TaskOrganizer.MapperProfiles;
public class TaskProfile : Profile
{
    public TaskProfile()
    {
        //CreateMap<TodoModel, Task>()
        //    .ForMember(dest => dest.Id, opt => opt.Ignore())
        //    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Task))
        //    .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
        //    .ForMember(dest => dest.FinishDate, opt => opt.MapFrom(src => src.DoneTaskDate))
        //    .ForMember(dest => dest.IsSelected, opt => opt.MapFrom(src => src.IsSelected));

        //CreateMap<Task, TodoModel>()
        //    .ForMember(dest => dest.Id, opt => opt.Ignore())
        //    .ForMember(dest => dest.Task, opt => opt.MapFrom(src => src.Description))
        //    .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
        //    .ForMember(dest => dest.DoneTaskDate, opt => opt.MapFrom(src => src.FinishDate))
        //    .ForMember(dest => dest.IsSelected, opt => opt.MapFrom(src => src.IsSelected));
    }
}
