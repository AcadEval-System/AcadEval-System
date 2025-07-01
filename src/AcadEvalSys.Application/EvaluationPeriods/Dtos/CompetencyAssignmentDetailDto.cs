namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public record CompetencyAssignmentDetailDto
{
    public required Guid AssignmentId { get; init; }
    
    public required Guid CompetencyId { get; init; }
    public required string CompetencyName { get; init; }
    
    public required Guid SubjectId { get; init; }
    public required string SubjectName { get; init; }

    public required string ProfessorName { get; init; }
} 