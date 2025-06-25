using AcadEvalSys.Application.Career.Commands.CreateCareer;
using AcadEvalSys.Application.Career.Commands.DeleteCareer;
using AcadEvalSys.Domain.Exceptions;
using AcadEvalSys.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.EvaluationPeriods.Commands.DeleteEvaluationPeriod;

public class DeleteEvaluationPeriodCommandHandler(ILogger<DeleteEvaluationPeriodCommandHandler> logger, IEvaluationPeriodRepository evaluationPeriodRepository) : IRequestHandler<DeleteEvaluationPeriodCommand>
{
    public async Task Handle(DeleteEvaluationPeriodCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting evaluation period with ID: {Id}", request.Id);
        
        var evaluationPeriod = await evaluationPeriodRepository.GetEvaluationPeriodByIdAsync(request.Id);

        if (evaluationPeriod == null)
        {
            throw new NotFoundException(nameof(EvaluationPeriods), request.Id.ToString());
        }
        
        await evaluationPeriodRepository.DeleteEvaluationPeriodAsync(request.Id);
        
        logger.LogInformation("Evaluation period with ID: {Id} deleted successfully", request.Id);
    }
}