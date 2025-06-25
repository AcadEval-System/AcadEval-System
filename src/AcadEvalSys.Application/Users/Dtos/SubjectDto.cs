namespace AcadEvalSys.Application.Users.Dtos;

public record SubjectDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Year { get; set; }
    public string? TechnicalCareerName { get; set; }
} 