using AcadEvalSys.Application.Career.Commands.CreateCareer;
using AcadEvalSys.Application.Career.Commands.DeleteCareer;
using AcadEvalSys.Application.Career.Commands.UpdateCareer;
using AcadEvalSys.Application.Career.Dtos;
using AcadEvalSys.Application.Career.Queries;
using AcadEvalSys.Application.Career.Queries.GetAllCareers;
using AcadEvalSys.Application.Career.Queries.GetCareerById;
using AcadEvalSys.Domain.Constants.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcadEvalSys.WEB.Server.Controllers;
[ApiController]
[Route("careers")]
[Authorize(Roles = UserRoles.Admin)] 
public class CareersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CareerDto>>> GetAllCareers()
    {
        var careers = await mediator.Send(new GetAllCareersQuery());
        return Ok(careers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CareerDto>> GetById([FromRoute]Guid id)
    {
        var career = await mediator.Send(new GetCareerByIdQuery(id));
        return Ok(career);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCareer(CreateCareerCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateCareer([FromRoute] Guid id, [FromBody] UpdateCareerCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCareer([FromRoute] Guid id)
    {
        await mediator.Send(new DeleteCareerCommand(id));
        return NoContent();
    }
}