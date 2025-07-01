using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.Professor.Commands.UpdateProfessor;

public class UpdateProfessorCommandHandler(ILogger<UpdateProfessorCommandHandler> logger, IMapper mapper, UserManager<User> userManager) : IRequestHandler<UpdateProfessorCommand, string>
{
    public async Task<string> Handle(UpdateProfessorCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating professor with id {Id}", request.Id);
        var user = userManager.FindByIdAsync(request.Id).Result;
        if (user == null)
        {
            logger.LogWarning("Professor with id {Id} not found", request.Id);
            throw new NotFoundException(nameof(Professor),request.Id);
        }
        user.UserName = request.Email;
        user.Email = request.Email;
        user.NormalizedUserName = request.Email.ToUpperInvariant();
        user.NormalizedEmail = request.Email.ToUpperInvariant();
        
        await userManager.UpdateAsync(user);
        logger.LogInformation("Successfully Updated professor with id {Id}", request.Id);
        return user.Id;
    }
}