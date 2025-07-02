using AcadEvalSys.Application.CompetenciesEvaluationInstances.Commands.CreateCompetenciesEvaluationInstance;
using AcadEvalSys.Application.CompetenciesEvaluationInstances.Commands.UpdateCompetenciesEvaluationInstance;
using AcadEvalSys.Domain.Entities;
using AutoMapper;

namespace AcadEvalSys.Application.CompetenciesEvaluationInstances.Dtos;

public class CompetenciesEvaluationInstanceProfile : Profile
{
    public CompetenciesEvaluationInstanceProfile()
    {
        // Command to Entity - AutoMapper mapea automáticamente: Title, Description, PeriodFrom, PeriodTo
        CreateMap<CreateCompetenciesEvaluationInstanceCommand, CompetenciesEvaluationInstance>();
        CreateMap<UpdateCompetenciesEvaluationInstanceCommand, CompetenciesEvaluationInstance>();
        CreateMap<CreateCompetencyAssignmentDto, ProfessorCompetencyAssignment>();

        // Entity to DTO - mapeo simplificado para el frontend
        CreateMap<CompetenciesEvaluationInstance, CompetenciesEvaluationInstanceDetailDto>()
            .ForMember(dest => dest.CareerAssignments, opt => opt.MapFrom(src =>
                src.ProfessorCompetencyAssignments != null
                    ? src.ProfessorCompetencyAssignments
                        .GroupBy(pca => new
                        {
                            TechnicalCareerId = pca.Subject != null && pca.Subject.TechnicalCareerId.HasValue
                                ? pca.Subject.TechnicalCareerId.Value
                                : Guid.Empty,
                            CareerName = pca.Subject != null && pca.Subject.TechnicalCareer != null
                                ? pca.Subject.TechnicalCareer.Name
                                : string.Empty
                        })
                        .Select(careerGroup => new CareerWithAssignmentsDto
                        {
                            TechnicalCareerId = careerGroup.Key.TechnicalCareerId,
                            TechnicalCareerName = careerGroup.Key.CareerName,
                            AssignmentsByYear = careerGroup.GroupBy(pca => pca.Subject.Year)
                                .Select(yearGroup => new CompetencyAssignmentByCareerYearDto()
                                {
                                    Year = yearGroup.Key,
                                    Assignments = yearGroup.Select(pca => new CompetencyAssignmentDetailDto
                                        {
                                            AssignmentId = pca.Id,
                                            CompetencyId = pca.CompetencyId,
                                            SubjectId = pca.SubjectId,
                                            CompetencyName = pca.Competency.Name,
                                            SubjectName = pca.Subject.Name,
                                            ProfessorName = pca.Subject.Professor.User.Name
                                        })
                                        .ToArray()
                                }).ToArray()
                        }
    )
    : new List<CareerWithAssignmentsDto>()));
    
}
} 