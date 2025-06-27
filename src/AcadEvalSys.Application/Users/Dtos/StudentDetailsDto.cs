using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Application.Users.Dtos;

public record StudentDetailsDto
{
    public Guid? TechnicalCareerId { get; set; }
    public string? TechnicalCareerName { get; set; }
    public CareerYear CurrentYear { get; set; }
    public IEnumerable<SubjectDetailsDto> Subjects { get; set; } = new List<SubjectDetailsDto>();
}