using MediatR;

namespace AcadEvalSys.Application.EvaluationPeriods.Commands.DeleteEvaluationPeriod;

public class DeleteEvaluationPeriodCommand(Guid id) : IRequest
{
    public Guid Id { get; } = id;
}