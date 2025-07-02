namespace AcadEvalSys.Application.CompetenciesEvaluationInstances.Dtos;

public record CompetenciesEvaluationInstanceDetailDto
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public string Description { get; init; } = string.Empty;
    public required DateTime PeriodFrom { get; init; }
    public required DateTime PeriodTo { get; init; }
    
    public IReadOnlyList<CareerWithAssignmentsDto> CareerAssignments { get; init; } = [];
} 