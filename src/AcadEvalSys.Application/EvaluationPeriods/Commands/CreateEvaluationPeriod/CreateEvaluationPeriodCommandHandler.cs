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
    IProfessorCompetencyAssignmentRepository professorCompetencyAssignmentRepository,
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

        var evaluationPeriod = mapper.Map<EvaluationPeriod>(request);
        evaluationPeriod.CreatedByUserId = user.Id ?? string.Empty;

        var evaluationPeriodId = await evaluationPeriodRepository.CreateEvaluationPeriodAsync(evaluationPeriod);

        logger.LogInformation("Evaluation period created successfully with ID: {Id}", evaluationPeriodId);

        var professorAssignments = new List<ProfessorCompetencyAssignment>();

        foreach (var assignment in request.Assignments)
        {
            var professorAssignment = mapper.Map<ProfessorCompetencyAssignment>(assignment);
            professorAssignment.EvaluationPeriodId = evaluationPeriodId;
            professorAssignment.CreatedByUserId = user.Id ?? string.Empty;

            professorAssignments.Add(professorAssignment);
        }

        if (professorAssignments.Any())
        {
            await professorCompetencyAssignmentRepository.CreateMultipleAsync(professorAssignments);
            logger.LogInformation("Created {Count} professor competency assignments", professorAssignments.Count);
        }

        return evaluationPeriodId;
    }
} 