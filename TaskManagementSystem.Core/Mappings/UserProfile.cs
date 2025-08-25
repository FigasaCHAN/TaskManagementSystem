using AutoMapper;
using TaskManagementSystem.Core.Domain.AggregateRoots;
using TaskManagementSystem.Core.Dtos.User;

namespace TaskManagementSystem.Core.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Username.Value))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value));
    }
}