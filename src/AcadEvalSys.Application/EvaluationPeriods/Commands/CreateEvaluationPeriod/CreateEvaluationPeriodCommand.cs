using AcadEvalSys.Application.EvaluationPeriods.Dtos;
using MediatR;

namespace AcadEvalSys.Application.EvaluationPeriods.Commands.CreateEvaluationPeriod;

public class CreateEvaluationPeriodCommand : IRequest<Guid>
{
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public required DateTime PeriodFrom { get; set; }
    public required DateTime PeriodTo { get; set; }
    public IReadOnlyList<CreateCompetencyAssignmentDto> Assignments { get; set; } = [];
} 