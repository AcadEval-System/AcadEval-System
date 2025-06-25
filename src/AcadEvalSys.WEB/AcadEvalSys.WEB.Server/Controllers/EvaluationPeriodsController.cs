using AcadEvalSys.Application.EvaluationPeriods.Commands.CreateEvaluationPeriod;
using AcadEvalSys.Application.EvaluationPeriods.Commands.DeleteEvaluationPeriod;
using AcadEvalSys.Application.EvaluationPeriods.Commands.UpdateEvaluationPeriod;
using AcadEvalSys.Application.EvaluationPeriods.Queries.GetEvaluationPeriod;
using AcadEvalSys.Domain.Constants.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcadEvalSys.WEB.Server.Controllers;

[ApiController]
[Route("evaluation-periods")]
[Authorize(Roles = UserRoles.Admin)]
public class EvaluationPeriodsController(IMediator mediator) : ControllerBase
{

    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEvaluationPeriodById(Guid id)
    {
        var evaluationPeriod = await mediator.Send(new GetEvaluationPeriodQuery(id));
        return Ok(evaluationPeriod);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvaluationPeriod([FromBody] CreateEvaluationPeriodCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetEvaluationPeriodById), new { id }, null);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvaluationPeriod([FromRoute] Guid id)
    {
        await mediator.Send(new DeleteEvaluationPeriodCommand(id));
        return NoContent();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvaluationPeriod([FromBody] UpdateEvaluationPeriodCommand command, [FromRoute] Guid id)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }
} 