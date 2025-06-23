using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public class CareerAssignmentDto
{
    public Guid TechnicalCareerId { get; set; }
    public Dictionary<CareerYear, List<CompetencyAssignmentDto>> AssignmentsByYear { get; set; } = new();
}