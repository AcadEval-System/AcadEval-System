using AcadEvalSys.Application.Users.Dtos;
using AcadEvalSys.Domain.Constants.Constants;
using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.Users.Queries.GetCurrentUserInfo;

public class GetCurrentUserInfoQueryHandler(
    ILogger<GetCurrentUserInfoQueryHandler> logger,
    UserManager<User> userManager,
    IMapper mapper,
    IUserContext userContext) : IRequestHandler<GetCurrentUserInfoQuery, CurrentUserDto>
{
    public async Task<CurrentUserDto> Handle(GetCurrentUserInfoQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting current user info");

        var currentUser = userContext.GetCurrentUser();
        
        if (currentUser is null) throw new UnauthorizedException();

        var user = await userManager.FindByIdAsync(currentUser.Id!);

        if (user is null) throw new NotFoundException(nameof(User), currentUser.Id!);

        if (currentUser.Roles is null || !currentUser.Roles.Any())
        {
            logger.LogWarning("User {UserId} has no roles assigned", currentUser.Id);
            throw new UnauthorizedException();
        }

        var result = mapper.Map<CurrentUserDto>(user);
        result.Roles = currentUser.Roles!;

        var userRole = currentUser.Roles.First();
        logger.LogInformation("Current user has role: {Role} with ID: {UserId}", userRole, currentUser.Id);

        return result;
    }
} 