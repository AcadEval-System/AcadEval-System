using MediatR;

namespace AcadEvalSys.Application.CompetenciesEvaluationInstances.Commands.UpdateCompetenciesEvaluationInstance;

public class UpdateCompetenciesEvaluationInstanceCommand : IRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime PeriodFrom { get; set; }
    public DateTime PeriodTo { get; set; }
} 