using AcadEvalSys.Application.Users.Dtos;
using MediatR;

namespace AcadEvalSys.Application.Users.Queries;

public class GetUserInfoQuery : IRequest<UserInfoDto>
{
}