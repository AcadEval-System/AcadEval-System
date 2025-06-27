using AcadEvalSys.Application.Users.Dtos;
using AcadEvalSys.Application.Users.Queries.GetCurrentUserInfo;
using AcadEvalSys.Application.Users.Services;
using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Application.Users.Queries.GetSessionStatus;

public class GetSessionStatusQueryHandler(
    ILogger<GetSessionStatusQueryHandler> logger,
    UserManager<User> userManager,
    IMediator mediator,
    IUserContext userContext,
    ISessionService sessionService) : IRequestHandler<GetSessionStatusQuery, SessionStatusDto>
{
    public async Task<SessionStatusDto> Handle(GetSessionStatusQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Checking session status");

        var currentUser = userContext.GetCurrentUser();
        
        // Si no hay usuario en contexto, no hay sesión válida
        if (currentUser is null)
        {
            logger.LogInformation("No current user context found");
            return new SessionStatusDto
            {
                IsAuthenticated = false
            };
        }

        try
        {
            var userInfo = await mediator.Send(new GetCurrentUserInfoQuery(), cancellationToken);
            
            // Usar el servicio para obtener la expiración real
            var sessionExpiration = sessionService.GetSessionExpiration();
            var minutesRemaining = sessionService.GetMinutesRemaining();

            logger.LogInformation("Session valid for user {UserId}, expires at {ExpiresAt}", 
                currentUser.Id, sessionExpiration);

            return new SessionStatusDto
            {
                IsAuthenticated = true,
                User = userInfo,
                ExpiresAt = sessionExpiration,
                MinutesRemaining = minutesRemaining
            };
        }
        catch (UnauthorizedException)
        {
            logger.LogInformation("User context exists but user is not authorized");
            return new SessionStatusDto
            {
                IsAuthenticated = false
            };
        }
        catch (NotFoundException)
        {
            logger.LogWarning("User {UserId} found in context but not in database", currentUser.Id);
            return new SessionStatusDto
            {
                IsAuthenticated = false
            };
        }
    }
} 