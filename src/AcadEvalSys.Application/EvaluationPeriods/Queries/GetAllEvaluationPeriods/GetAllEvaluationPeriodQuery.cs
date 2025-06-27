using AcadEvalSys.Application.EvaluationPeriods.Dtos;
using MediatR;

namespace AcadEvalSys.Application.EvaluationPeriods.Queries.GetAllEvaluationPeriods;

public class GetAllEvaluationPeriodQuery : IRequest<IEnumerable<EvaluationPeriodDetailDto>>
{
}