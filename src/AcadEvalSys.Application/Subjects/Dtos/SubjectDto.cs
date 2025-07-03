using AcadEvalSys.Application.Students.Dtos;
using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Application.Subjects.Dtos;

public class SubjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid TechnicalCareerId { get; set; }
    public string? TechnicalCareerName { get; set; }
    public CareerYear Year { get; set; }
    public string? ProfessorId { get; set; }
    public string? ProfessorName { get; set; }
    public IEnumerable<StudentDto> Students { get; set; } = new List<StudentDto>();
}