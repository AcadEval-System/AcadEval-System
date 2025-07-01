using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Application.Users.Dtos;

public record SubjectDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public CareerYear Year { get; set; }
    public string? TechnicalCareerName { get; set; }
} 