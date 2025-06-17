using AcadEvalSys.Application.EvaluationPeriods.Dtos;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.EvaluationPeriods.Queries.GetAllEvaluationPeriods;

public class GetAllEvaluationPeriodsQueryHandler(
    ILogger<GetAllEvaluationPeriodsQueryHandler> logger,
    IEvaluationPeriodRepository evaluationPeriodRepository,
    IMapper mapper) : IRequestHandler<GetAllEvaluationPeriodsQuery, IEnumerable<EvaluationPeriodDto>>
{
    public async Task<IEnumerable<EvaluationPeriodDto>> Handle(GetAllEvaluationPeriodsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all evaluation periods");

        var evaluationPeriods = await evaluationPeriodRepository.GetAllEvaluationPeriodsAsync();
        var result = mapper.Map<IEnumerable<EvaluationPeriodDto>>(evaluationPeriods);

        logger.LogInformation("Retrieved {Count} evaluation periods", result.Count());

        return result;
    }
} 