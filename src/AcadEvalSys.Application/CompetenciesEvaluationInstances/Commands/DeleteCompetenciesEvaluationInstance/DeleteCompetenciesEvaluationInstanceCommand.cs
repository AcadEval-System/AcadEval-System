using MediatR;

namespace AcadEvalSys.Application.CompetenciesEvaluationInstances.Commands.DeleteCompetenciesEvaluationInstance;

public class DeleteCompetenciesEvaluationInstanceCommand(Guid id) : IRequest
{
    public Guid Id { get; } = id;
} 