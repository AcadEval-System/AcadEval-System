using AcadEvalSys.Application.Users;
using AcadEvalSys.Domain.Exceptions;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.Competencies.Commands.UpdateCompetency;

public class UpdateCompetencyCommandHandler(ILogger<UpdateCompetencyCommandHandler> logger, ICompetencyRepository competencyRepository, IMapper mapper, IUserContext userContext)  : IRequestHandler<UpdateCompetencyCommand>
{
    public async Task Handle(UpdateCompetencyCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating competency with ID: {Id} with {@request}", request.Id, request);

        var user = userContext.GetCurrentUser();
         if (user == null)
        {
            logger.LogError("User context is null despite controller authorization - possible system configuration error");
            throw new InvalidOperationException("User context not found");
        }

        var existingCompetency = await competencyRepository.GetCompetencyByIdAsync(request.Id);
        
        if (existingCompetency == null)
        {
            logger.LogWarning("Competency with ID: {Id} not found", request.Id);
            throw new InvalidOperationException($"Competency with ID {request.Id} was not found.");
        }

        if (existingCompetency.Name != request.Name)
        {
            var nameExists = await competencyRepository.ExistsByNameAsync(request.Name);
            if (nameExists)
            {
                logger.LogWarning("Competency with name '{Name}' already exists", request.Name);
                throw new InvalidOperationException($"A competency with the name '{request.Name}' already exists.");
            }
        }

        mapper.Map(request, existingCompetency);
        
        existingCompetency.UpdatedAt = DateTime.UtcNow;
        existingCompetency.UpdatedByUserId = user.Id ?? String.Empty;

        await competencyRepository.UpdateCompetencyAsync(existingCompetency);

        logger.LogInformation("Competency with ID: {Id} updated successfully", request.Id);
    }
}