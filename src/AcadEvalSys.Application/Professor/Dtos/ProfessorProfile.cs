using AcadEvalSys.Application.Professor.Commands.CreateProfessor;
using AcadEvalSys.Application.Professor.Commands.UpdateProfessor;
using AcadEvalSys.Domain.Entities;
using AutoMapper;

namespace AcadEvalSys.Application.Professor.Dtos;

public class ProfessorProfile : Profile
{
    public ProfessorProfile()
    {
        CreateMap<Domain.Entities.Professor, ProfessorDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

        CreateMap<CreateProfessorCommand, User>()
            .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
            .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.Name.ToUpper()));

        CreateMap<UpdateProfessorCommand, User>();

    }
}