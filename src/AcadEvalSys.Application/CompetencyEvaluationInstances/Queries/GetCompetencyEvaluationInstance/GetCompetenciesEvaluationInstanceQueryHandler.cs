using AcadEvalSys.Application.CompetencyEvaluationInstances.Dtos;
using AcadEvalSys.Application.CompetencyEvaluationInstances.Queries.GetCompetencyEvaluationInstance;
using AcadEvalSys.Domain.Exceptions;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.CompetencyEvaluationInstances.Queries.GetCompetencyEvaluationInstance;

public class GetCompetencyEvaluationInstanceQueryHandler(
    ILogger<GetCompetencyEvaluationInstanceQueryHandler> logger,
    ICompetencyEvaluationInstanceRepository competencyEvaluationInstanceRepository,
    IMapper mapper) : IRequestHandler<GetCompetencyEvaluationInstanceQuery, CompetencyEvaluationInstanceDetailDto>
{
    public async Task<CompetencyEvaluationInstanceDetailDto> Handle(GetCompetencyEvaluationInstanceQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting CompetenciesEvaluationInstance with ID: {Id}", request.Id);

        var competencyEvaluationInstance = await competencyEvaluationInstanceRepository.GetCompetencyEvaluationInstanceByIdAsync(request.Id);

        if (competencyEvaluationInstance == null)
        {
            throw new NotFoundException("CompetenciesEvaluationInstance", request.Id.ToString());
        }

        var competencyEvaluationInstanceDto = mapper.Map<CompetencyEvaluationInstanceDetailDto>(competencyEvaluationInstance);
        logger.LogInformation("CompetencyEvaluationInstance with ID {Id} found", request.Id);

        return competencyEvaluationInstanceDto;
    }
}