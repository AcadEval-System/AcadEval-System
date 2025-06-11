using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.Competencies.Commands.UpdateCompetency;

public class UpdateCompetencyCommandHandler(ILogger<UpdateCompetencyCommandHandler> logger, ICompetencyRepository competencyRepository, IMapper mapper)  : IRequestHandler<UpdateCompetencyCommand>
{
    public async Task Handle(UpdateCompetencyCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating competency with ID: {Id} with {@request}", request.Id, request);

        var existingCompetency = await competencyRepository.GetCompetencyByIdAsync(request.Id);
        
        if (existingCompetency == null)
        {
            logger.LogWarning("Competency with ID: {Id} not found", request.Id);
            throw new InvalidOperationException($"Competency with ID {request.Id} was not found.");
        }

        mapper.Map(request, existingCompetency);
        existingCompetency.UpdatedAt = DateTime.UtcNow;

        await competencyRepository.UpdateCompetencyAsync(existingCompetency);

        logger.LogInformation("Competency with ID: {Id} updated successfully", request.Id);
    }
}