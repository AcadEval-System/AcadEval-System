using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Application.Subjects.Dtos;

public record SubjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public CareerYear Year { get; set; }
    public string? TechnicalCareerName { get; set; }
}