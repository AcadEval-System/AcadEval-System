using AcadEvalSys.Application.Students.Commands.AddStudent;
using AcadEvalSys.Domain.Entities;
using AutoMapper;

namespace AcadEvalSys.Application.Students.Dtos;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        // Mapeo de comando a entidad User
        CreateMap<AddStudentCommand, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => true));
  }
}