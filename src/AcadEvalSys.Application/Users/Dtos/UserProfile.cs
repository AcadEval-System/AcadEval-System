using AcadEvalSys.Domain.Entities;
using AutoMapper;

namespace AcadEvalSys.Application.Users.Dtos;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Usuario, UserInfoDto>();
    }
}