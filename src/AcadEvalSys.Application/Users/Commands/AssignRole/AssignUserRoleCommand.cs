using MediatR;

namespace AcadEvalSys.Application.Users.Commands.AssignRole;

public class AssignUserRoleCommand() : IRequest
{
    public string UserEmail { get; set; } = null!;
    public string RoleName { get; set; } = null!;
}