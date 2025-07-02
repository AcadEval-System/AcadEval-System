using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.Professor.Commands.DeleteProfessor;

public class RemoveProfessorCommandHandler(ILogger<RemoveProfessorCommandHandler> logger, UserManager<User> userManager) : IRequestHandler<RemoveProfessorCommand>
{
    public async Task Handle(RemoveProfessorCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting professor with id {Id}", request.Id);
        var user = userManager.FindByIdAsync(request.Id).Result;
        if (user == null)
        {
            logger.LogWarning("Professor with id {Id} not found", request.Id);
            throw new NotFoundException(nameof(Professor),request.Id);
        }
        await userManager.DeleteAsync(user);
        logger.LogInformation("Successfully deleted professor with id {Id}", request.Id);
    }
}