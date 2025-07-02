using AcadEvalSys.Application.Professor.Commands.CreateProfessor;
using AcadEvalSys.Domain.Entities;
using AutoMapper;

namespace AcadEvalSys.Application.Professor.Dtos;

public class ProfessorProfile : Profile
{
    public ProfessorProfile()
    {
        CreateMap<AddProfessorCommand, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Professor, opt => opt.Ignore()); // Ignore navigation property
    }
}