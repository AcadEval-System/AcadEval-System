using AcadEvalSys.Application.Users.Commands.AssignRole;
using AcadEvalSys.Application.Users.Commands.UnassignUserRole;
using AcadEvalSys.Domain.Constants.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcadEvalSys.WEB.Server.Controllers;

[Route("identity")]
[Tags("Identity")]
[ApiController]
public class IdentityController(IMediator mediator) : ControllerBase
{
  
    [HttpPost("userRole")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("userRole")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> UnassignUserRole(UnassingUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}