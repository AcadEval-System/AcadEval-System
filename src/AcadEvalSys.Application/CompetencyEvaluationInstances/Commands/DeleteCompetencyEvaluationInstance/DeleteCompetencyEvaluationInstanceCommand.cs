using MediatR;

namespace AcadEvalSys.Application.CompetencyEvaluationInstances.Commands.DeleteCompetencyEvaluationInstance;

public class DeleteCompetencyEvaluationInstanceCommand(Guid id) : IRequest
{
    public Guid Id { get; } = id;
}