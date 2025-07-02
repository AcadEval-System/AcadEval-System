using AcadEvalSys.Application.CompetenciesEvaluationInstances.Dtos;
using MediatR;

namespace AcadEvalSys.Application.CompetenciesEvaluationInstances.Commands.CreateCompetenciesEvaluationInstance;

public class CreateCompetenciesEvaluationInstanceCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime PeriodFrom { get; set; }
    public DateTime PeriodTo { get; set; }
    public CreateCompetencyAssignmentDto[] CompetencyAssignments { get; set; } = [];
} 