namespace AcadEvalSys.Application.Users.Dtos;

public record ProfessorDetailsDto
{
    public string? Phone { get; set; }
    public IEnumerable<SubjectDetailsDto> Subjects { get; set; } = new List<SubjectDetailsDto>();
} 