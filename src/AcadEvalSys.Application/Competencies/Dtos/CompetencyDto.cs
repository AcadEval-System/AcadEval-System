using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Application.Competencies.Dtos;

public class CompetencyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public CompetencyType Type { get; set; } 
    public IReadOnlyCollection<CompetencyLevelDescriptionDto> Levels { get; set; } = [];
}