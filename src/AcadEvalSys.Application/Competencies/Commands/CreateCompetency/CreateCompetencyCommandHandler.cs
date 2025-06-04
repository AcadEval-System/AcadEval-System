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

        var competency = mapper.Map<Competency>(request);

        competency.CareerCompetencies = request.CareerIds.Select(careerId => new CareerCompetencies
        {
            CareerId = careerId,
            CompetencyId = competency.Id
        }).ToList();

        var id = await competencyRepository.CreateCompetencyAsync(competency);

        if (competency.CareerCompetencies != null && competency.CareerCompetencies.Any())
        {
            await competencyRepository.AddCareerCompetenciesAsync(competency.CareerCompetencies);
        }

        logger.LogInformation("Competency created successfully with ID: {Id}", id);
        
        return id;
    }
}