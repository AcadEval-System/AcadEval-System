using AcadEvalSys.Application.Users.Commands.AssignRole;
using AcadEvalSys.Application.Users.Commands.UnassignUserRole;
using AcadEvalSys.Application.Users.Queries;
using AcadEvalSys.Application.Users.Queries.GetCurrentUserInfo;
using AcadEvalSys.Domain.Constants.Constants;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AcadEvalSys.WEB.Server.Controllers;

[Route("identity")]
[Tags("Identity")]
[ApiController]
public class IdentityController(IMediator mediator) : ControllerBase
{
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        return NoContent();
    }
        
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

    [HttpGet("info")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var user = await mediator.Send(new GetCurrentUserInfoQuery());
        return Ok(user);
    }
}