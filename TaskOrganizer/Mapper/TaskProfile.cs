using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Model;

namespace TaskOrganizer.Mapper
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TodoModel, TaskModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TaskDesc, opt => opt.MapFrom(src => src.Task))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.DoneTaskDate, opt => opt.MapFrom(src => src.DoneTaskDate))
                .ForMember(dest => dest.IsSelected, opt => opt.MapFrom(src => src.IsSelected));

            CreateMap<TaskModel, TodoModel>()
                .ForMember(dest => dest.TaskID, opt => opt.Ignore())
                .ForMember(dest => dest.Task, opt => opt.MapFrom(src => src.TaskDesc))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.DoneTaskDate, opt => opt.MapFrom(src => src.DoneTaskDate))
                .ForMember(dest => dest.IsSelected, opt => opt.MapFrom(src => src.IsSelected));
        }
    }
}
