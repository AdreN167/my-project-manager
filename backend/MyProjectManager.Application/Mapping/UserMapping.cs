using AutoMapper;
using MyProjectManager.Domain.Dto.User;
using MyProjectManager.Domain.Entity;

namespace MyProjectManager.Application.Mapping;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}
