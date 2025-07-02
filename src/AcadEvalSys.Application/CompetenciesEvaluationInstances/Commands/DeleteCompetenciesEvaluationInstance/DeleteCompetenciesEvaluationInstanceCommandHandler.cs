using AcadEvalSys.Domain.Exceptions;
using AcadEvalSys.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.CompetenciesEvaluationInstances.Commands.DeleteCompetenciesEvaluationInstance;

public class DeleteCompetenciesEvaluationInstanceCommandHandler(
    ILogger<DeleteCompetenciesEvaluationInstanceCommandHandler> logger,
    ICompetenciesEvaluationInstanceRepository competenciesEvaluationInstanceRepository) : IRequestHandler<DeleteCompetenciesEvaluationInstanceCommand>
{
    public async Task Handle(DeleteCompetenciesEvaluationInstanceCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting CompetenciesEvaluationInstance with ID: {Id}", request.Id);

        var competenciesEvaluationInstance = await competenciesEvaluationInstanceRepository.GetCompetenciesEvaluationInstanceByIdAsync(request.Id);

        if (competenciesEvaluationInstance == null)
        {
            throw new NotFoundException("CompetenciesEvaluationInstance", request.Id.ToString());
        }

        await competenciesEvaluationInstanceRepository.DeleteCompetenciesEvaluationInstanceAsync(request.Id);

        logger.LogInformation("CompetenciesEvaluationInstance with ID {Id} deleted successfully", request.Id);
    }
} 