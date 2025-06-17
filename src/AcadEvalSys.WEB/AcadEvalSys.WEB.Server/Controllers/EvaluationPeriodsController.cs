using AcadEvalSys.Application.EvaluationPeriods.Commands.CreateEvaluationPeriod;
using AcadEvalSys.Application.EvaluationPeriods.Queries.GetAllEvaluationPeriods;
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
    [HttpGet]
    public async Task<IActionResult> GetEvaluationPeriods()
    {
        var evaluationPeriods = await mediator.Send(new GetAllEvaluationPeriodsQuery());
        return Ok(evaluationPeriods);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetEvaluationPeriod(Guid id)
    {
        var evaluationPeriod = await mediator.Send(new GetEvaluationPeriodQuery(id));
        return Ok(evaluationPeriod);
    }


    [HttpPost]
    public async Task<IActionResult> CreateEvaluationPeriod([FromBody] CreateEvaluationPeriodCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetEvaluationPeriod), new { id }, new { id });
    }
    

    [HttpPut("{evaluationPeriodId}/open")]
    public async Task<IActionResult> OpenEvaluationPeriod(Guid evaluationPeriodId)
    {
        // TODO: Implementar comando para abrir período de evaluación
        // await mediator.Send(new OpenEvaluationPeriodCommand(evaluationPeriodId));
        return Ok(new { message = "Período de evaluación abierto exitosamente" });
    }


    [HttpPut("{evaluationPeriodId}/close")]
    public async Task<IActionResult> CloseEvaluationPeriod(Guid evaluationPeriodId)
    {
        // TODO: Implementar comando para cerrar período de evaluación
        // await mediator.Send(new CloseEvaluationPeriodCommand(evaluationPeriodId));
        return Ok(new { message = "Período de evaluación cerrado exitosamente" });
    }
} 