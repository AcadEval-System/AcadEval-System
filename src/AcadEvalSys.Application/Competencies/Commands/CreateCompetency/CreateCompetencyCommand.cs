using AcadEvalSys.Domain.Enums;
using MediatR;

namespace AcadEvalSys.Application.Competencies.Commands.CreateCompetency;

public class CreateCompetencyCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public CompetencyType Type { get; set; }
}