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
        
        // Entity to DTO - mapeo simplificado para el frontend
        CreateMap<EvaluationPeriod, EvaluationPeriodDetailDto>()
            .ForMember(dest => dest.CareerAssignments, opt => opt.MapFrom(src =>
                src.ProfessorCompetencyAssignments != null
                    ? src.ProfessorCompetencyAssignments
                        .GroupBy(pca => new
                        {
                            pca.TechnicalCareerId,
                            CareerName = pca.TechnicalCareer != null ? pca.TechnicalCareer.Name : string.Empty
                        })
                        .Select(careerGroup => new CareerWithAssignmentsDto
                        {
                            TechnicalCareerId = careerGroup.Key.TechnicalCareerId,
                            TechnicalCareerName = careerGroup.Key.CareerName,
                            AssignmentsByYear = careerGroup
                                .GroupBy(pca => pca.Year)
                                .ToDictionary(
                                    yearGroup => yearGroup.Key,
                                    yearGroup => yearGroup.Select(pca => new CompetencyAssignmentDetailDto
                                    {
                                        AssignmentId = pca.Id,
                                        Year = pca.Year,
                                        TechnicalCareerId = pca.TechnicalCareerId,
                                        TechnicalCareerName = pca.TechnicalCareer != null
                                            ? pca.TechnicalCareer.Name
                                            : string.Empty,
                                        CompetencyId = pca.CompetencyId,
                                        CompetencyName = pca.Competency != null ? pca.Competency.Name : string.Empty,
                                        CompetencyDescription = pca.Competency != null
                                            ? pca.Competency.Description
                                            : string.Empty,
                                        CompetencyType = pca.Competency != null ? pca.Competency.Type : default,
                                        ProfessorId = pca.ProfessorId,
                                        ProfessorName = pca.Professor != null && pca.Professor.User != null
                                            ? pca.Professor.User.UserName
                                            : string.Empty,
                                        ProfessorEmail = pca.Professor != null && pca.Professor.User != null
                                            ? pca.Professor.User.Email
                                            : string.Empty
                                    }).ToList()
                                ),
                            TotalAssignments = careerGroup.Count(),
                            TotalProfessors = careerGroup.Select(pca => pca.ProfessorId).Distinct().Count(),
                            TotalCompetencies = careerGroup.Select(pca => pca.CompetencyId).Distinct().Count(),
                            ActiveYears = careerGroup.Select(pca => pca.Year).Distinct().ToList()
                        }).ToList()
                    : new List<CareerWithAssignmentsDto>()));
        

        // Mapeo detallado para cada asignación de competencia
        CreateMap<ProfessorCompetencyAssignment, CompetencyAssignmentDetailDto>()
            .ForMember(dest => dest.AssignmentId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.TechnicalCareerId, opt => opt.MapFrom(src => src.TechnicalCareerId))
            .ForMember(dest => dest.TechnicalCareerName, opt => opt.MapFrom(src => src.TechnicalCareer != null ? src.TechnicalCareer.Name : string.Empty))
            .ForMember(dest => dest.CompetencyId, opt => opt.MapFrom(src => src.CompetencyId))
            .ForMember(dest => dest.CompetencyName, opt => opt.MapFrom(src => src.Competency != null ? src.Competency.Name : string.Empty))
            .ForMember(dest => dest.CompetencyDescription, opt => opt.MapFrom(src => src.Competency != null ? src.Competency.Description : string.Empty))
            .ForMember(dest => dest.CompetencyType, opt => opt.MapFrom(src => src.Competency != null ? src.Competency.Type : default))
            .ForMember(dest => dest.ProfessorName, opt => opt.MapFrom(src => src.Professor != null && src.Professor.User != null ? 
                src.Professor.User.UserName : string.Empty))
            .ForMember(dest => dest.ProfessorEmail, opt => opt.MapFrom(src => src.Professor != null && src.Professor.User != null ? 
                src.Professor.User.Email : string.Empty));

        // Mapeo para resumen de carreras
        CreateMap<TechnicalCareer, CareerSummaryDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.TotalAssignments, opt => opt.MapFrom(src => 
                src.ProfessorCompetencyAssignments != null ? src.ProfessorCompetencyAssignments.Count : 0))
            .ForMember(dest => dest.TotalProfessors, opt => opt.MapFrom(src => 
                src.ProfessorCompetencyAssignments != null ? src.ProfessorCompetencyAssignments.Select(pca => pca.ProfessorId).Distinct().Count() : 0))
            .ForMember(dest => dest.TotalCompetencies, opt => opt.MapFrom(src => 
                src.ProfessorCompetencyAssignments != null ? src.ProfessorCompetencyAssignments.Select(pca => pca.CompetencyId).Distinct().Count() : 0));

        // DTO to Entity - para crear ProfessorCompetencyAssignment desde CompetencyAssignmentDto
        CreateMap<CompetencyAssignmentDto, ProfessorCompetencyAssignment>()
            .ForMember(dest => dest.EvaluationPeriodId, opt => opt.Ignore()) // Se establece en lógica de negocio
            .ForMember(dest => dest.TechnicalCareerId, opt => opt.Ignore()) // Se establece en lógica de negocio
            .ForMember(dest => dest.Year, opt => opt.Ignore()); // Se establece en lógica de negocio

        // Entity to DTO - para leer datos
        CreateMap<ProfessorCompetencyAssignment, CompetencyAssignmentDto>();
    }
} 