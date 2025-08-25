using AutoMapper;
using TaskManagementSystem.Core.Domain.Entities.Task;
using TaskManagementSystem.Core.Dtos.Task;
using Task = TaskManagementSystem.Core.Domain.AggregateRoots.Task;

namespace TaskManagementSystem.Core.Mappings;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<Task, TaskDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.CreatedBy))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedBy.CreatedAt))
            .ForMember(dest => dest.LastModifiedBy,
                opt => opt.MapFrom(src => src.LastModified != null ? src.LastModified.LastModifiedBy : null))
            .ForMember(dest => dest.LastModifiedAt,
                opt => opt.MapFrom(src => src.LastModified != null ? src.LastModified.LastModifiedAt : null));
        CreateMap<TaskHistory, TaskHistoryDto>();
    }
}