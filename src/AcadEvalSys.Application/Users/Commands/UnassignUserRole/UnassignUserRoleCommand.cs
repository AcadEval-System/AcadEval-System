namespace AcadEvalSys.Application.Users.Commands.UnassignUserRole;

using MediatR;

public class UnassingUserRoleCommand : IRequest
{
    public string UserEmail { get; set; } = null!;
    public string RoleName { get; set; } = null!;
}