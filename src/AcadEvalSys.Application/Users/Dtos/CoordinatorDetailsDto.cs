namespace AcadEvalSys.Application.Users.Dtos;

public record CoordinatorDetailsDto
{
    public Guid? TechnicalCareerId { get; set; }
    public string? TechnicalCareerName { get; set; }
} 