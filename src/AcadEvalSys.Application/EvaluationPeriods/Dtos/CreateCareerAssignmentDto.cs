using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public record CreateCareerAssignmentDto
{
    public required Guid TechnicalCareerId { get; init; }
    public Dictionary<CareerYear, List<CreateCompetencyAssignmentDto>> AssignmentsByYear { get; init; } = new();
}