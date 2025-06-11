using AcadEvalSys.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.Competencies.Commands.DeleteCompetency;

public class DeleteCompetencyCommandHandler(ILogger<DeleteCompetencyCommandHandler> logger, ICompetencyRepository competencyRepository) : IRequestHandler<DeleteCompetencyCommand>
{
    public async Task Handle(DeleteCompetencyCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting competency with ID: {Id}", request.Id);
        
        await competencyRepository.DeleteCompetencyAsync(request.Id);
        
        logger.LogInformation("Competency with ID: {Id} deleted successfully", request.Id);
    }
}