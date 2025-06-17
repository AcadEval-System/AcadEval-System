using AcadEvalSys.Application.EvaluationPeriods.Dtos;
using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Exceptions;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.EvaluationPeriods.Queries.GetEvaluationPeriod;

public class GetEvaluationPeriodQueryHandler(
    ILogger<GetEvaluationPeriodQueryHandler> logger,
    IEvaluationPeriodRepository evaluationPeriodRepository,
    IMapper mapper) : IRequestHandler<GetEvaluationPeriodQuery, EvaluationPeriodDto>
{
    public async Task<EvaluationPeriodDto> Handle(GetEvaluationPeriodQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting evaluation period with ID: {Id}", request.Id);

        var evaluationPeriod = await evaluationPeriodRepository.GetEvaluationPeriodByIdAsync(request.Id);
        if (evaluationPeriod == null)
        {
            logger.LogWarning("Evaluation period with ID '{Id}' not found", request.Id);
            throw new NotFoundException(nameof(EvaluationPeriod), request.Id.ToString());
        }

        var result = mapper.Map<EvaluationPeriodDto>(evaluationPeriod);

        logger.LogInformation("Retrieved evaluation period: {Title}", evaluationPeriod.Title);

        return result;
    }
} 