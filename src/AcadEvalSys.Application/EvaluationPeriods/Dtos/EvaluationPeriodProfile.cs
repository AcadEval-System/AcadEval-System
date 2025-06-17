using AcadEvalSys.Application.EvaluationPeriods.Commands.CreateEvaluationPeriod;
using AcadEvalSys.Domain.Entities;
using AutoMapper;

namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public class EvaluationPeriodProfile : Profile
{
    public EvaluationPeriodProfile()
    {
        // Command to Entity mappings
        CreateMap<CreateEvaluationPeriodCommand, EvaluationPeriod>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedByUserId, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedByUserId, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.ProfessorCompetencyAssignments, opt => opt.Ignore())
            .ForMember(dest => dest.EvaluationPeriodCareers, opt => opt.Ignore())
            .ForMember(dest => dest.StudentEvaluationReports, opt => opt.Ignore())
            .ForMember(dest => dest.TechnicalCareers, opt => opt.Ignore());

        // Entity to DTO mappings
        CreateMap<EvaluationPeriod, EvaluationPeriodDto>()
            .ForMember(dest => dest.TechnicalCareers, opt => opt.MapFrom(src => src.TechnicalCareers))
            .ForMember(dest => dest.ProfessorAssignments, opt => opt.MapFrom(src => src.ProfessorCompetencyAssignments));

        CreateMap<TechnicalCareer, TechnicalCareerDto>();

      
    }
} 