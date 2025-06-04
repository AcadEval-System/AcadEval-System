using MediatR;

namespace AcadEvalSys.Application.Competencies.Commands.CreateCompetency;

public class CreateCompetencyCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public ICollection<Guid> CareerIds { get; set; } = new List<Guid>();
}