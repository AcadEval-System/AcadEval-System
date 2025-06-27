using AcadEvalSys.Application.EvaluationPeriods.Dtos;
using MediatR;

namespace AcadEvalSys.Application.EvaluationPeriods.Queries.GetEvaluationPeriod;

public class GetEvaluationPeriodQuery(Guid id) : IRequest<EvaluationPeriodDetailDto>
{
    public Guid Id { get; } = id;
}