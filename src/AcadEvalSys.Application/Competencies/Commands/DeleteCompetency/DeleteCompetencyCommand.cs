using MediatR;

namespace AcadEvalSys.Application.Competencies.Commands.DeleteCompetency;

public class DeleteCompetencyCommand(Guid id) : IRequest
{
    public Guid Id { get; } = id;
}