using AcadEvalSys.Application.Users;
using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Exceptions;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.EvaluationPeriods.Commands.CreateEvaluationPeriod;

public class CreateEvaluationPeriodCommandHandler(
    ILogger<CreateEvaluationPeriodCommandHandler> logger,
    IEvaluationPeriodRepository evaluationPeriodRepository,
    ICareerRepository careerRepository,
    IMapper mapper,
    IUserContext userContext) : IRequestHandler<CreateEvaluationPeriodCommand, Guid>
{
    public async Task<Guid> Handle(CreateEvaluationPeriodCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating evaluation period {@EvaluationPeriod}", request);

        var user = userContext.GetCurrentUser();
        if (user == null)
        {
            logger.LogError("User context is null despite controller authorization");
            throw new InvalidOperationException("User context not found");
        }

        var existingPeriod = await evaluationPeriodRepository.ExistsByTitleAsync(request.Title);
        if (existingPeriod)
        {
            logger.LogWarning("Evaluation period with title '{Title}' already exists", request.Title);
            throw new DuplicateResourceException(nameof(EvaluationPeriod), request.Title);
        }


        // Create evaluation period
        var evaluationPeriod = mapper.Map<EvaluationPeriod>(request);
        evaluationPeriod.CreatedByUserId = user.Id ?? string.Empty;

        var id = await evaluationPeriodRepository.CreateEvaluationPeriodAsync(evaluationPeriod);

        logger.LogInformation("Evaluation period created successfully with ID: {Id}", id);

        return id;
    }
} 