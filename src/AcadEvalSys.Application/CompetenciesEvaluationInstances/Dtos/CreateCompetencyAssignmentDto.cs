namespace AcadEvalSys.Application.CompetenciesEvaluationInstances.Dtos;

public record CreateCompetencyAssignmentDto
{
    public required Guid CompetencyId { get; init; }
    public required Guid SubjectId { get; init; }
} 