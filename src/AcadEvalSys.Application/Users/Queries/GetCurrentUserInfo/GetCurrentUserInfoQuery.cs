using AcadEvalSys.Application.Users.Dtos;
using MediatR;

namespace AcadEvalSys.Application.Users.Queries.GetCurrentUserInfo;

public class GetCurrentUserInfoQuery : IRequest<CurrentUserDto>; 