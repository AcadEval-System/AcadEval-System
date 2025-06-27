namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public record CreateCompetencyAssignmentDto
{
    public required Guid CompetencyId { get; init; }
    public required string ProfessorId { get; init; }
}