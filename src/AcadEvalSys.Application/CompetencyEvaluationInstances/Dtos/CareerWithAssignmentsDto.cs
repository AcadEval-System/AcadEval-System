using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Application.CompetenciesEvaluationInstances.Dtos;

public record CareerWithAssignmentsDto
{
    public required Guid TechnicalCareerId { get; init; }
    public required string TechnicalCareerName { get; init; }
    public CompetencyAssignmentByCareerYearDto[] AssignmentsByYear { get; init; } = [];
}