using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.Users.Commands.AssignRole;

public class AssignUserRoleCommandHandler(
    ILogger<AssignUserRoleCommandHandler> logger,
    UserManager<Usuario> userManager,
    RoleManager<IdentityRole> roleManager) : IRequestHandler<AssignUserRoleCommand>
{
    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Assigning role {RoleName} to user with email {UserEmail}",
            request.RoleName, request.UserEmail);
        var user = await userManager.FindByEmailAsync(request.UserEmail) ??
                   throw new NotFoundException(nameof(Usuario), request.UserEmail);
        var role = await roleManager.FindByNameAsync(request.RoleName) ??
                   throw new NotFoundException(nameof(IdentityRole), request.RoleName);

        await userManager.AddToRoleAsync(user, role.Name!);
    }
}