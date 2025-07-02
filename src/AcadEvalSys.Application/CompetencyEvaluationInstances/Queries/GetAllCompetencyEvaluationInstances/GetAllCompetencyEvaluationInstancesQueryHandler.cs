using AcadEvalSys.Application.CompetenciesEvaluationInstances.Dtos;
using AcadEvalSys.Application.CompetenciesEvaluationInstances.Queries.GetAllCompetenciesEvaluationInstances;
using AcadEvalSys.Application.CompetencyEvaluationInstances.Dtos;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.CompetencyEvaluationInstances.Queries.GetAllCompetencyEvaluationInstances;

public class GetAllCompetencyEvaluationInstancesQueryHandler(
    ILogger<GetAllCompetencyEvaluationInstancesQueryHandler> logger,
    ICompetencyEvaluationInstanceRepository competencyEvaluationInstanceRepository,
    IMapper mapper) : IRequestHandler<GetAllCompetencyEvaluationInstancesQuery, IEnumerable<CompetencyEvaluationInstanceDetailDto>>
{
    public async Task<IEnumerable<CompetencyEvaluationInstanceDetailDto>> Handle(GetAllCompetencyEvaluationInstancesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all CompetencyEvaluationInstances");

        var competencyEvaluationInstances = await competencyEvaluationInstanceRepository.GetAllCompetencyEvaluationInstancesAsync();

        if (competencyEvaluationInstances == null || !competencyEvaluationInstances.Any())
        {
            logger.LogInformation("No CompetencyEvaluationInstances found");
            return Enumerable.Empty<CompetencyEvaluationInstanceDetailDto>();
        }

        var competencyEvaluationInstanceDtos = mapper.Map<IEnumerable<CompetencyEvaluationInstanceDetailDto>>(competencyEvaluationInstances);
        logger.LogInformation("Found {Count} CompetencyEvaluationInstances", competencyEvaluationInstanceDtos.Count());

        return competencyEvaluationInstanceDtos;
    }
}