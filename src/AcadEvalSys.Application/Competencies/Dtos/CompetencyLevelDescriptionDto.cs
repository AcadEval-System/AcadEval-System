using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Application.Competencies.Dtos;

public class CompetencyLevelDescriptionDto
{
    public CompetencyLevel Level { get; set; }
    public string Description { get; set; } = string.Empty;
}