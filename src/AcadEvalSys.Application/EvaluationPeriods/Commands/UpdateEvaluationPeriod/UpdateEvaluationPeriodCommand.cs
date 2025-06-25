using MediatR;

namespace AcadEvalSys.Application.EvaluationPeriods.Commands.UpdateEvaluationPeriod;

public class UpdateEvaluationPeriodCommand : IRequest
{   
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public required DateTime PeriodFrom { get; set; }
    public required DateTime PeriodTo { get; set; }
}