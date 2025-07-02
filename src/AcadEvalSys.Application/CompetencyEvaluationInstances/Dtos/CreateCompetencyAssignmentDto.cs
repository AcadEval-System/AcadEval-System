namespace AcadEvalSys.Application.CompetencyEvaluationInstances.Dtos;

public record CreateCompetencyAssignmentDto
{
    public required Guid CompetencyId { get; init; }
    public required Guid SubjectId { get; init; }
}