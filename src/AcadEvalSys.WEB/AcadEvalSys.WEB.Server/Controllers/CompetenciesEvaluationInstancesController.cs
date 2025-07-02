using AcadEvalSys.Application.CompetenciesEvaluationInstances.Commands.CreateCompetenciesEvaluationInstance;
using AcadEvalSys.Application.CompetenciesEvaluationInstances.Commands.DeleteCompetenciesEvaluationInstance;
using AcadEvalSys.Application.CompetenciesEvaluationInstances.Commands.UpdateCompetenciesEvaluationInstance;
using AcadEvalSys.Application.CompetenciesEvaluationInstances.Queries.GetAllCompetenciesEvaluationInstances;
using AcadEvalSys.Application.CompetenciesEvaluationInstances.Queries.GetCompetenciesEvaluationInstance;
using AcadEvalSys.Domain.Constants.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcadEvalSys.WEB.Server.Controllers;

[ApiController]
[Route("competencies-evaluation-instances")]
[Authorize(Roles = UserRoles.Admin)]
public class CompetenciesEvaluationInstancesController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAllCompetenciesEvaluationInstances()
    {
        var competenciesEvaluationInstances = await mediator.Send(new GetAllCompetenciesEvaluationInstancesQuery());
        return Ok(competenciesEvaluationInstances);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompetenciesEvaluationInstanceById(Guid id)
    {
        var competenciesEvaluationInstance = await mediator.Send(new GetCompetenciesEvaluationInstanceQuery(id));
        return Ok(competenciesEvaluationInstance);
    }


    [HttpPost]
    public async Task<IActionResult> CreateCompetenciesEvaluationInstance([FromBody] CreateCompetenciesEvaluationInstanceCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetCompetenciesEvaluationInstanceById), new { id }, new { id });
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompetenciesEvaluationInstance([FromRoute] Guid id)
    {
        await mediator.Send(new DeleteCompetenciesEvaluationInstanceCommand(id));
        return NoContent();
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompetenciesEvaluationInstance([FromBody] UpdateCompetenciesEvaluationInstanceCommand command, [FromRoute] Guid id)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }
}