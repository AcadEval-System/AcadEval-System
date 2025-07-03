using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Exceptions;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.CompetencyEvaluationInstances.Commands.UpdateCompetencyEvaluationInstance;

public class UpdateCompetencyEvaluationInstanceCommandHandler(
    ILogger<UpdateCompetencyEvaluationInstanceCommandHandler> logger,
    ICompetencyEvaluationInstanceRepository competencyEvaluationInstanceRepository,
    IMapper mapper) : IRequestHandler<UpdateCompetencyEvaluationInstanceCommand>
{
    public async Task Handle(UpdateCompetencyEvaluationInstanceCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating CompetencyEvaluationInstance with ID: {Id}", request.Id);

        var existingCompetencyEvaluationInstance = await competencyEvaluationInstanceRepository.GetCompetencyEvaluationInstanceByIdAsync(request.Id);

        if (existingCompetencyEvaluationInstance == null)
        {
            throw new NotFoundException(nameof(CompetencyEvaluationInstance), request.Id.ToString());
        }

        mapper.Map(request, existingCompetencyEvaluationInstance);
        existingCompetencyEvaluationInstance.UpdatedAt = DateTime.UtcNow;

        await competencyEvaluationInstanceRepository.UpdateCompetencyEvaluationInstanceAsync(existingCompetencyEvaluationInstance);

        logger.LogInformation("CompetencyEvaluationInstance with ID {Id} updated successfully", request.Id);
    }
}