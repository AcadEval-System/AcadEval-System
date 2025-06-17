using AcadEvalSys.Application.EvaluationPeriods.Dtos;
using MediatR;

namespace AcadEvalSys.Application.EvaluationPeriods.Queries.GetAllEvaluationPeriods;

public class GetAllEvaluationPeriodsQuery : IRequest<IEnumerable<EvaluationPeriodDto>>
{
} 