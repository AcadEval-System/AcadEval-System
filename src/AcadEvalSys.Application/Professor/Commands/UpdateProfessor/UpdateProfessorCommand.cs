using MediatR;

namespace AcadEvalSys.Application.Professor.Commands.UpdateProfessor;

public class UpdateProfessorCommand(string id) : IRequest<string>
{
    public string Id { get; set; } = id;
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}