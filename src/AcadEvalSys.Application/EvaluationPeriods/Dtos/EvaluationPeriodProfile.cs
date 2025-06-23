using AcadEvalSys.Application.EvaluationPeriods.Commands.CreateEvaluationPeriod;
using AcadEvalSys.Domain.Entities;
using AutoMapper;

namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public class EvaluationPeriodProfile : Profile
{
    public EvaluationPeriodProfile()
    {
        // Command to Entity - AutoMapper mapea automáticamente: Title, Description, PeriodFrom, PeriodTo
        CreateMap<CreateEvaluationPeriodCommand, EvaluationPeriod>();

        // DTO to Entity - para crear ProfessorCompetencyAssignment desde CompetencyAssignmentDto
        CreateMap<CompetencyAssignmentDto, ProfessorCompetencyAssignment>()
            .ForMember(dest => dest.EvaluationPeriodId, opt => opt.Ignore()) // Se establece en lógica de negocio
            .ForMember(dest => dest.TechnicalCareerId, opt => opt.Ignore()) // Se establece en lógica de negocio
            .ForMember(dest => dest.Year, opt => opt.Ignore()); // Se establece en lógica de negocio

        // Entity to DTO - para leer datos
        CreateMap<ProfessorCompetencyAssignment, CompetencyAssignmentDto>();
    }
} 