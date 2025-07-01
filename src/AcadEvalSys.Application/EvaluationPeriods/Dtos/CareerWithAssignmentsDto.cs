using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public record CareerWithAssignmentsDto
{
    public required Guid TechnicalCareerId { get; init; }
    public required string TechnicalCareerName { get; init; }
    public CompetencyAssignmentDetailDto[] Assignments { get; init; } = [];
    public int TotalAssignments { get; init; }
    public int TotalProfessors { get; init; }
    public int TotalCompetencies { get; init; }
} 