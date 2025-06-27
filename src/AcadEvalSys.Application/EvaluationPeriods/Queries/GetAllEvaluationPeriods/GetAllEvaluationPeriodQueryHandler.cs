using AcadEvalSys.Application.EvaluationPeriods.Dtos;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.EvaluationPeriods.Queries.GetAllEvaluationPeriods;

public class GetAllEvaluationPeriodQueryHandler(ILogger<GetAllEvaluationPeriodQueryHandler> logger, IEvaluationPeriodRepository evaluationPeriodRepository, IMapper mapper) : IRequestHandler<GetAllEvaluationPeriodQuery, IEnumerable<EvaluationPeriodDetailDto>>
{
    public async Task<IEnumerable<EvaluationPeriodDetailDto>> Handle(GetAllEvaluationPeriodQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all evaluation periods");
        var evaluationPeriods = await evaluationPeriodRepository.GetAllEvaluationPeriodsAsync();
        if (evaluationPeriods == null || !evaluationPeriods.Any())
        {
            logger.LogWarning("No evaluation periods found");
            return Enumerable.Empty<EvaluationPeriodDetailDto>();
        }
        var evaluationPeriodDto = mapper.Map<IEnumerable<EvaluationPeriodDetailDto>>(evaluationPeriods);
        logger.LogInformation("Found {Count} evaluation periods", evaluationPeriodDto.Count());
        return evaluationPeriodDto;
    }
}