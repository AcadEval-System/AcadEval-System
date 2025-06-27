using AcadEvalSys.Application.Competencies.Commands.CreateCompetency;
using AcadEvalSys.Application.Competencies.Commands.DeleteCompetency;
using AcadEvalSys.Application.Competencies.Commands.UpdateCompetency;
using AcadEvalSys.Application.Competencies.Queries.GetAllCompetencies;
using AcadEvalSys.Application.Competencies.Queries.GetCompetency;
using AcadEvalSys.Domain.Constants.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcadEvalSys.WEB.Server.Controllers;

[ApiController]
[Route("competencies")]
[Authorize(Roles = UserRoles.Admin)]
public class CompetenciesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCompetencies()
    {
        var competencies = await mediator.Send(new GetAllCompetenciesQuery());
        return Ok(competencies);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompetency(Guid id)
    {
        var competency = await mediator.Send(new GetCompetencyQuery(id));
        return Ok(competency);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompetency([FromBody] CreateCompetencyCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetCompetency), new { id }, null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompetency([FromRoute] Guid id, [FromBody] UpdateCompetencyCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompetency(Guid id)
    {
        await mediator.Send(new DeleteCompetencyCommand(id));
        return NoContent();
    }
}