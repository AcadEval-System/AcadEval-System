using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Exceptions;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.CompetenciesEvaluationInstances.Commands.UpdateCompetenciesEvaluationInstance;

public class UpdateCompetenciesEvaluationInstanceCommandHandler(
    ILogger<UpdateCompetenciesEvaluationInstanceCommandHandler> logger,
    ICompetenciesEvaluationInstanceRepository competenciesEvaluationInstanceRepository,
    IMapper mapper) : IRequestHandler<UpdateCompetenciesEvaluationInstanceCommand>
{
    public async Task Handle(UpdateCompetenciesEvaluationInstanceCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating CompetenciesEvaluationInstance with ID: {Id}", request.Id);

        var existingCompetenciesEvaluationInstance = await competenciesEvaluationInstanceRepository.GetCompetenciesEvaluationInstanceByIdAsync(request.Id);

        if (existingCompetenciesEvaluationInstance == null)
        {
            throw new NotFoundException(nameof(CompetenciesEvaluationInstance), request.Id.ToString());
        }

        mapper.Map(request, existingCompetenciesEvaluationInstance);
        existingCompetenciesEvaluationInstance.UpdatedAt = DateTime.UtcNow;

        await competenciesEvaluationInstanceRepository.UpdateCompetenciesEvaluationInstanceAsync(existingCompetenciesEvaluationInstance);

        logger.LogInformation("CompetenciesEvaluationInstance with ID {Id} updated successfully", request.Id);
    }
} 