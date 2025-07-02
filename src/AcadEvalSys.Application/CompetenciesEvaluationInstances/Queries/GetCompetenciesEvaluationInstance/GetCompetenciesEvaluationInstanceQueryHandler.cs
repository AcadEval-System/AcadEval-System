using AcadEvalSys.Application.CompetenciesEvaluationInstances.Dtos;
using AcadEvalSys.Domain.Exceptions;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.CompetenciesEvaluationInstances.Queries.GetCompetenciesEvaluationInstance;

public class GetCompetenciesEvaluationInstanceQueryHandler(
    ILogger<GetCompetenciesEvaluationInstanceQueryHandler> logger,
    ICompetenciesEvaluationInstanceRepository competenciesEvaluationInstanceRepository,
    IMapper mapper) : IRequestHandler<GetCompetenciesEvaluationInstanceQuery, CompetenciesEvaluationInstanceDetailDto>
{
    public async Task<CompetenciesEvaluationInstanceDetailDto> Handle(GetCompetenciesEvaluationInstanceQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting CompetenciesEvaluationInstance with ID: {Id}", request.Id);

        var competenciesEvaluationInstance = await competenciesEvaluationInstanceRepository.GetCompetenciesEvaluationInstanceByIdAsync(request.Id);

        if (competenciesEvaluationInstance == null)
        {
            throw new NotFoundException("CompetenciesEvaluationInstance", request.Id.ToString());
        }

        var competenciesEvaluationInstanceDto = mapper.Map<CompetenciesEvaluationInstanceDetailDto>(competenciesEvaluationInstance);
        logger.LogInformation("CompetenciesEvaluationInstance with ID {Id} found", request.Id);

        return competenciesEvaluationInstanceDto;
    }
} 