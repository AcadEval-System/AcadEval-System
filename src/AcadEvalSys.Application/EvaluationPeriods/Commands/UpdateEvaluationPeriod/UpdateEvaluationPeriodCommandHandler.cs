using AcadEvalSys.Application.Competencies.Commands.UpdateCompetency;
using AcadEvalSys.Application.Users;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.EvaluationPeriods.Commands.UpdateEvaluationPeriod;

public class UpdateEvaluationPeriodCommandHandler (ILogger<UpdateEvaluationPeriodCommandHandler> logger, IEvaluationPeriodRepository evaluationPeriodRepository, IMapper mapper)  : IRequestHandler<UpdateEvaluationPeriodCommand>
{
    public async Task Handle(UpdateEvaluationPeriodCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating evaluation period with ID: {Id} with {@request}", request.Id, request);
        
        var existingEvaluationPeriod = await evaluationPeriodRepository.GetEvaluationPeriodByIdAsync(request.Id);
        
        if (existingEvaluationPeriod == null)
        {
            logger.LogWarning("Evaluation period with ID: {Id} not found", request.Id);
            throw new InvalidOperationException($"Evaluation period with ID {request.Id} was not found.");
        }

        if (existingEvaluationPeriod.Title != request.Title)
        {
            var titleExists = await evaluationPeriodRepository.ExistsByTitleAsync(request.Title);
            if (titleExists)
            {
                logger.LogWarning("Evaluation period with title '{Title}' already exists", request.Title);
                throw new InvalidOperationException($"An Evaluation period with the title '{request.Title}' already exists.");
            }
        }

        mapper.Map(request, existingEvaluationPeriod);
        existingEvaluationPeriod.UpdatedAt = DateTime.UtcNow;
        
        await evaluationPeriodRepository.UpdateEvaluationPeriodAsync(existingEvaluationPeriod);

        logger.LogInformation("Evaluation period with ID: {Id} updated successfully", request.Id);
    }
}