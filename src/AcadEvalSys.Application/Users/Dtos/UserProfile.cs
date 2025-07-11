using AcadEvalSys.Domain.Entities;
using AutoMapper;

namespace AcadEvalSys.Application.Users.Dtos;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, CurrentUserDto>()
            .ForMember(dest => dest.Roles, opt => opt.Ignore());

        CreateMap<Student, StudentDetailsDto>()
            .ForMember(dest => dest.TechnicalCareerName, opt => opt.MapFrom(src => src.TechnicalCareer!.Name))
            .ForMember(dest => dest.Subjects, opt => opt.Ignore());

        CreateMap<Professor, ProfessorDetailsDto>()
            .ForMember(dest => dest.Subjects, opt => opt.Ignore());

        CreateMap<Coordinator, CoordinatorDetailsDto>()
            .ForMember(dest => dest.TechnicalCareerName, opt => opt.MapFrom(src => src.TechnicalCareer!.Name));
    }
}

