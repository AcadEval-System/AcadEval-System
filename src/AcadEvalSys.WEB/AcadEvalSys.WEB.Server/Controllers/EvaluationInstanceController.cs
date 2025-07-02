using AcadEvalSys.Application.CompetenciesEvaluationInstances.Queries.GetAllCompetenciesEvaluationInstances;
using AcadEvalSys.Application.CompetencyEvaluationInstances.Commands.CreateCompetencyEvaluationInstance;
using AcadEvalSys.Application.CompetencyEvaluationInstances.Commands.DeleteCompetencyEvaluationInstance;
using AcadEvalSys.Application.CompetencyEvaluationInstances.Commands.UpdateCompetencyEvaluationInstance;
using AcadEvalSys.Application.CompetencyEvaluationInstances.Queries.GetAllCompetencyEvaluationInstances;
using AcadEvalSys.Application.CompetencyEvaluationInstances.Queries.GetCompetencyEvaluationInstance;
using AcadEvalSys.Domain.Constants.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcadEvalSys.WEB.Server.Controllers;

[ApiController]
[Route("evaluation-instances")]
[Authorize(Roles = UserRoles.Admin)]
public class EvaluationInstanceController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAllEvaluationInstances()
    {
        var competenciesEvaluationInstances = await mediator.Send(new GetAllCompetencyEvaluationInstancesQuery());
        return Ok(competenciesEvaluationInstances);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetEvaluationInstanceById(Guid id)
    {
        var competenciesEvaluationInstance = await mediator.Send(new GetCompetencyEvaluationInstanceQuery(id));
        return Ok(competenciesEvaluationInstance);
    }


    [HttpPost]
    public async Task<IActionResult> CreateEvaluationInstance([FromBody] CreateCompetencyEvaluationInstanceCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetEvaluationInstanceById), new { id }, new { id });
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvaluationInstance([FromRoute] Guid id)
    {
        await mediator.Send(new DeleteCompetencyEvaluationInstanceCommand(id));
        return NoContent();
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvaluationInstance([FromBody] UpdateCompetencyEvaluationInstanceCommand command, [FromRoute] Guid id)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }
}