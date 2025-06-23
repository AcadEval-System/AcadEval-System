using AcadEvalSys.Application.EvaluationPeriods.Dtos;
using AcadEvalSys.Domain.Exceptions;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.EvaluationPeriods.Queries.GetEvaluationPeriod;

public class GetEvaluationPeriodQueryHandler(
    ILogger<GetEvaluationPeriodQueryHandler> logger,
    IEvaluationPeriodRepository evaluationPeriodRepository,
    IMapper mapper
    ) : IRequestHandler<GetEvaluationPeriodQuery, CompetencyEvaluationDto>
{
    public async Task<CompetencyEvaluationDto> Handle(GetEvaluationPeriodQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting Evaluation for ID: {Id}", request.Id);
        var evaluationPeriod = await evaluationPeriodRepository.GetEvaluationPeriodByIdAsync(request.Id);
        if (evaluationPeriod == null)
        {
            logger.LogWarning("Evaluation with ID: {Id} not found", request.Id);
            throw new NotFoundException(nameof(evaluationPeriod), request.Id.ToString());
        }
        
        var evaluationDto = mapper.Map<CompetencyEvaluationDto>(evaluationPeriod);
        return evaluationDto;
    }
}