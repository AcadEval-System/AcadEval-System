using MediatR;

namespace AcadEvalSys.Application.Professor.Commands.CreateProfessor;

public class AddProfessorCommand : IRequest<string>
{
    public string Name { get; set; } 
    public string Email { get; set; }
    public string Password { get; set; }
}