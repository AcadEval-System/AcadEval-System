using AcadEvalSys.Application.Professor.Commands.CreateProfessor;
using AcadEvalSys.Domain.Entities;
using AutoMapper;

namespace AcadEvalSys.Application.Professor.Dtos;

public class ProfessorProfile : Profile
{
    public ProfessorProfile()
    {
        CreateMap<CreateProfessorCommand, User>()
            .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
            .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.Name.ToUpper()));
    }
}