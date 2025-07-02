using AcadEvalSys.Application.CompetenciesEvaluationInstances.Dtos;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.CompetenciesEvaluationInstances.Queries.GetAllCompetenciesEvaluationInstances;

public class GetAllCompetenciesEvaluationInstancesQueryHandler(
    ILogger<GetAllCompetenciesEvaluationInstancesQueryHandler> logger,
    ICompetenciesEvaluationInstanceRepository competenciesEvaluationInstanceRepository,
    IMapper mapper) : IRequestHandler<GetAllCompetenciesEvaluationInstancesQuery, IEnumerable<CompetenciesEvaluationInstanceDetailDto>>
{
    public async Task<IEnumerable<CompetenciesEvaluationInstanceDetailDto>> Handle(GetAllCompetenciesEvaluationInstancesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all CompetenciesEvaluationInstances");

        var competenciesEvaluationInstances = await competenciesEvaluationInstanceRepository.GetAllCompetenciesEvaluationInstancesAsync();
        
        if (competenciesEvaluationInstances == null || !competenciesEvaluationInstances.Any())
        {
            logger.LogInformation("No CompetenciesEvaluationInstances found");
            return Enumerable.Empty<CompetenciesEvaluationInstanceDetailDto>();
        }

        var competenciesEvaluationInstanceDtos = mapper.Map<IEnumerable<CompetenciesEvaluationInstanceDetailDto>>(competenciesEvaluationInstances);
        logger.LogInformation("Found {Count} CompetenciesEvaluationInstances", competenciesEvaluationInstanceDtos.Count());
        
        return competenciesEvaluationInstanceDtos;
    }
} 