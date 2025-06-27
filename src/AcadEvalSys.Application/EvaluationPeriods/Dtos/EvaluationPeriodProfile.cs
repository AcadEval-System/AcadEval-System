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
                                        CompetencyId = pca.CompetencyId,
                                        CompetencyName = pca.Competency != null ? pca.Competency.Name : string.Empty,
                                        CompetencyDescription = pca.Competency != null ? pca.Competency.Description : string.Empty,
                                        CompetencyType = pca.Competency != null ? pca.Competency.Type : default,
                                        ProfessorId = pca.ProfessorId,
                                        ProfessorName = pca.Professor != null && pca.Professor.User != null ? pca.Professor.User.UserName : string.Empty,
                                        ProfessorEmail = pca.Professor != null && pca.Professor.User != null ? pca.Professor.User.Email : string.Empty
                                    }).ToList()
                                ),
                            TotalAssignments = careerGroup.Count(),
                            TotalProfessors = careerGroup.Select(pca => pca.ProfessorId).Distinct().Count(),
                            TotalCompetencies = careerGroup.Select(pca => pca.CompetencyId).Distinct().Count(),
                            ActiveYears = careerGroup.Select(pca => pca.Year).Distinct().ToArray()
                        }).ToArray()
                    : Array.Empty<CareerWithAssignmentsDto>()));
        
        // Mapeo detallado para cada asignación de competencia
        CreateMap<ProfessorCompetencyAssignment, CompetencyAssignmentDetailDto>()
            .ForMember(dest => dest.AssignmentId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CompetencyId, opt => opt.MapFrom(src => src.CompetencyId))
            .ForMember(dest => dest.CompetencyName, opt => opt.MapFrom(src => src.Competency != null ? src.Competency.Name : string.Empty))
            .ForMember(dest => dest.CompetencyDescription, opt => opt.MapFrom(src => src.Competency != null ? src.Competency.Description : string.Empty))
            .ForMember(dest => dest.CompetencyType, opt => opt.MapFrom(src => src.Competency != null ? src.Competency.Type : default))
            .ForMember(dest => dest.ProfessorName, opt => opt.MapFrom(src => src.Professor != null && src.Professor.User != null ? 
                src.Professor.User.UserName : string.Empty))
            .ForMember(dest => dest.ProfessorEmail, opt => opt.MapFrom(src => src.Professor != null && src.Professor.User != null ? 
                src.Professor.User.Email : string.Empty));

        // DTO to Entity - para crear ProfessorCompetencyAssignment desde CompetencyAssignmentDto
        CreateMap<CreateCompetencyAssignmentDto, ProfessorCompetencyAssignment>()
            .ForMember(dest => dest.EvaluationPeriodId, opt => opt.Ignore()) // Se establece en lógica de negocio
            .ForMember(dest => dest.TechnicalCareerId, opt => opt.Ignore()) // Se establece en lógica de negocio
            .ForMember(dest => dest.Year, opt => opt.Ignore()); // Se establece en lógica de negocio

        // Entity to DTO - para leer datos
        CreateMap<ProfessorCompetencyAssignment, CreateCompetencyAssignmentDto>();
    }
} 