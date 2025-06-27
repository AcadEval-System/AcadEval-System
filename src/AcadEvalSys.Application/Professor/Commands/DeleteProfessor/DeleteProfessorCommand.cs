using MediatR;

namespace AcadEvalSys.Application.Professor.Commands.DeleteProfessor;

public class DeleteProfessorCommand(string id) : IRequest
{
    public string Id { get; } = id;
}