using AcadEvalSys.Application.Career.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AcadEvalSys.WEB.Server.Controllers;
[ApiController]
[Route("careers")]
public class CareersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCompetencies()
    {
        var careers = await mediator.Send(new GetAllCareersQuery());
        return Ok(careers);
    }
}