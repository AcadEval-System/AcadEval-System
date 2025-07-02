using AcadEvalSys.Application.Professor.Commands.CreateProfessor;
using AcadEvalSys.Application.Professor.Commands.UpdateProfessor;
using AcadEvalSys.Application.Professor.Dtos;
using AcadEvalSys.Application.Professor.Queries.GetAllProfessors;
using AcadEvalSys.Application.Professor.Queries.GetProfessor;
using AcadEvalSys.Domain.Constants.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcadEvalSys.WEB.Server.Controllers;

[ApiController]
[Route("professors")]
[Authorize(Roles = UserRoles.Admin)] 
public class ProfessorController(IMediator mediator) : ControllerBase
{
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProfessorDto>>> GetAllCareers()
    {
        var professors = await mediator.Send(new GetAllProfessorsQuery());
        return Ok(professors);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ProfessorDto>> GetById([FromRoute] Guid id)
    {
        var professor = await mediator.Send(new GetProfessorQuery(id));
        if (professor == null)
        {
            return NotFound();
        }
        return Ok(professor);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProfessor([FromBody] AddProfessorCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProfessor([FromRoute] string id, [FromBody] UpdateProfessorCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfessor([FromRoute] string id)
    {
        await mediator.Send(new AcadEvalSys.Application.Professor.Commands.DeleteProfessor.RemoveProfessorCommand(id));
        return NoContent();
    }
    
}