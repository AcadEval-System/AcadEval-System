using AcadEvalSys.Application.Users.Dtos;
using MediatR;

namespace AcadEvalSys.Application.Users.Queries.GetSessionStatus;


public record GetSessionStatusQuery : IRequest<SessionStatusDto>
{
} 