using AcadEvalSys.Application.Users.Dtos;
using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AcadEvalSys.Application.Users.Queries;

public class GetUserInfoQueryHandler(
    IUserContext userContext,
    IUserStore<User> userStore,
    IMapper mapper) : IRequestHandler<GetUserInfoQuery, UserInfoDto>
{
    public async Task<UserInfoDto> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        if (currentUser is null) throw new UnauthorizedException();

        var user = await userStore.FindByIdAsync(currentUser.Id!, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), currentUser.Id!);
        }

        return mapper.Map<UserInfoDto>(user);
    }
}