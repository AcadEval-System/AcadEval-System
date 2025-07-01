using AcadEvalSys.Application.Students.Commands.AddStudent;
using AcadEvalSys.Domain.Constants.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcadEvalSys.WEB.Server.Controllers
{
    [ApiController]
    [Route("students")]
    [Authorize(Roles = UserRoles.Admin)]
    public class StudentController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] AddStudentCommand command)
        {
       // Logic to create a student
            var result = await mediator.Send(command);
            return CreatedAtAction(null, new { id = result }, null);
        }
        
    }
}