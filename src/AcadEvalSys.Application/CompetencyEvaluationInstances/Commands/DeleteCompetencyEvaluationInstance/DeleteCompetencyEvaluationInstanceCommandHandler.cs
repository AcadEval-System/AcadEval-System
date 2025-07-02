using AcadEvalSys.Domain.Exceptions;
using AcadEvalSys.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.CompetencyEvaluationInstances.Commands.DeleteCompetencyEvaluationInstance;

public class DeleteCompetencyEvaluationInstanceCommandHandler(
    ILogger<DeleteCompetencyEvaluationInstanceCommandHandler> logger,
    ICompetencyEvaluationInstanceRepository competencyEvaluationInstanceRepository) : IRequestHandler<DeleteCompetencyEvaluationInstanceCommand>
{
    public async Task Handle(DeleteCompetencyEvaluationInstanceCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting CompetencyEvaluationInstance with ID: {Id}", request.Id);

        var competencyEvaluationInstance = await competencyEvaluationInstanceRepository.GetCompetencyEvaluationInstanceByIdAsync(request.Id);

        if (competencyEvaluationInstance == null)
        {
            throw new NotFoundException("CompetencyEvaluationInstance", request.Id.ToString());
        }

        await competencyEvaluationInstanceRepository.DeleteCompetencyEvaluationInstanceAsync(request.Id);

        logger.LogInformation("CompetencyEvaluationInstance with ID {Id} deleted successfully", request.Id);
    }
}