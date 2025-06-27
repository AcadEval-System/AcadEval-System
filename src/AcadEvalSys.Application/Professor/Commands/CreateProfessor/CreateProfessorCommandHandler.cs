using AcadEvalSys.Domain.Constants.Constants;
using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.Professor.Commands.CreateProfessor;

public class CreateProfessorCommandHandler(ILogger<CreateProfessorCommandHandler> logger, IMapper mapper, UserManager<User> userManager) : IRequestHandler<CreateProfessorCommand, string>
{
    public async Task<string> Handle(CreateProfessorCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new Professor {Name}", request.Name);
        var user = mapper.Map<User>(request);
        user.UserName = request.Email;
        user.Email = request.Email;
        user.NormalizedUserName = request.Email.ToUpperInvariant();
        user.NormalizedEmail = request.Email.ToUpperInvariant();
        var result = await userManager.CreateAsync(user, request.Password);
        var userCreated = await userManager.FindByEmailAsync(request.Email);
        // role
        await userManager.AddToRoleAsync(userCreated, UserRoles.Professor);
        
        logger.LogInformation("Successfully created Professor {Name} with ID {UserId}", request.Name, user.Id);
        return userCreated.Id;
    }
}