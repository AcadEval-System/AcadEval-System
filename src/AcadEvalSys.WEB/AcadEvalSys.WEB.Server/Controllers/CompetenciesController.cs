using AcadEvalSys.Application.Competencies.Commands.CreateCompetency;
using AcadEvalSys.Application.Competencies.Queries.GetAllCompetencies;
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

    /*[HttpPost]
    public async Task<IActionResult> CreateCompetency([FromBody] CreateCompetencyCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetCompetency), new { id = command.Id }, command);
    }*/



}