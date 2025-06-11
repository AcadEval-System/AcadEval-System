using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.Competencies.Commands.CreateCompetency;

public class CreateCompetencyCommandHandler(ILogger<CreateCompetencyCommandHandler> logger, ICompetencyRepository competencyRepository, IMapper mapper) : IRequestHandler<CreateCompetencyCommand, Guid>
{
    public async Task<Guid> Handle(CreateCompetencyCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating competency {@Competency}", request);

        var existingCompetency = await competencyRepository.ExistsByNameAsync(request.Name);
       
        if (existingCompetency)
        {
            logger.LogWarning("Competency with name '{Name}' already exists", request.Name);
            throw new InvalidOperationException($"A competency with the name '{request.Name}' already exists.");
        }

        var competency = mapper.Map<Competency>(request);
        var id = await competencyRepository.CreateCompetencyAsync(competency);

        logger.LogInformation("Competency created successfully with ID: {Id}", id);
        
        return id;
    }
}