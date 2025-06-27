using MediatR;

namespace AcadEvalSys.Application.Professor.Commands.CreateProfessor;

public class CreateProfessorCommand : IRequest<string>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}