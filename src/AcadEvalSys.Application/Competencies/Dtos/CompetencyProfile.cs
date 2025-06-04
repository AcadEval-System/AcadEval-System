using AcadEvalSys.Application.Career.Commands;
using AcadEvalSys.Domain.Entities;
using AutoMapper;

namespace AcadEvalSys.Application.Competencies.Dtos;

public class CompetencyProfile : Profile    
{
    public CompetencyProfile()
    {
        CreateMap<CreateCareerCommand, Competency>();

        CreateMap<Competency, CompetencyDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));
    }
}