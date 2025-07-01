using AcadEvalSys.Application.EvaluationPeriods.Commands.CreateEvaluationPeriod;
using AcadEvalSys.Application.EvaluationPeriods.Commands.UpdateEvaluationPeriod;
using AcadEvalSys.Domain.Entities;
using AutoMapper;

namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public class EvaluationPeriodProfile : Profile
{
    public EvaluationPeriodProfile()
    {
        // Command to Entity - AutoMapper mapea automáticamente: Title, Description, PeriodFrom, PeriodTo
        CreateMap<CreateEvaluationPeriodCommand, EvaluationPeriod>();
        CreateMap<UpdateEvaluationPeriodCommand, EvaluationPeriod>();
        
        // Entity to DTO - mapeo simplificado para el frontend
        CreateMap<EvaluationPeriod, EvaluationPeriodDetailDto>()
            .ForMember(dest => dest.CareerAssignments, opt => opt.MapFrom(src =>
                src.ProfessorCompetencyAssignments != null
                    ? src.ProfessorCompetencyAssignments
                        .GroupBy(pca => new
                        {
                            TechnicalCareerId = pca.Subject != null && pca.Subject.TechnicalCareerId.HasValue ? pca.Subject.TechnicalCareerId.Value : Guid.Empty,
                            CareerName = pca.Subject != null && pca.Subject.TechnicalCareer != null ? pca.Subject.TechnicalCareer.Name : string.Empty
                        })
                        .Select(careerGroup => new CareerWithAssignmentsDto
                        {
                            TechnicalCareerId = careerGroup.Key.TechnicalCareerId,
                            TechnicalCareerName = careerGroup.Key.CareerName,
                            Assignments = careerGroup.Select(pca => new CompetencyAssignmentDetailDto
                            {
                                AssignmentId = pca.Id,
                                CompetencyId = pca.CompetencyId,
                                SubjectId = pca.SubjectId
                            }).ToArray(),
                            TotalAssignments = careerGroup.Count(),
                            TotalProfessors = careerGroup.Select(pca => pca.Subject != null ? pca.Subject.ProfessorId : string.Empty).Distinct().Count(),
                            TotalCompetencies = careerGroup.Select(pca => pca.CompetencyId).Distinct().Count()
                        }).ToArray()
                    : Array.Empty<CareerWithAssignmentsDto>()));
        
        // Mapeo detallado para cada asignación de competencia - solo IDs
        CreateMap<ProfessorCompetencyAssignment, CompetencyAssignmentDetailDto>()
            .ForMember(dest => dest.AssignmentId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CompetencyId, opt => opt.MapFrom(src => src.CompetencyId))
            .ForMember(dest => dest.SubjectId, opt => opt.MapFrom(src => src.SubjectId));

        // DTO to Entity - para crear ProfessorCompetencyAssignment desde CompetencyAssignmentDto
        CreateMap<CreateCompetencyAssignmentDto, ProfessorCompetencyAssignment>()
            .ForMember(dest => dest.EvaluationPeriodId, opt => opt.Ignore()) // Se establece en lógica de negocio
            .ForMember(dest => dest.SubjectId, opt => opt.MapFrom(src => src.SubjectId));

        // Entity to DTO - para leer datos
        CreateMap<ProfessorCompetencyAssignment, CreateCompetencyAssignmentDto>();
    }
} 