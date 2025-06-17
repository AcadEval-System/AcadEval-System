namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public class EvaluationPeriodDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime PeriodFrom { get; set; }
    public DateTime PeriodTo { get; set; }
    public bool NotifyStart { get; set; }
    public bool SendReminders { get; set; }
    public bool NotifyClose { get; set; }
    public string? ReminderFrequency { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<TechnicalCareerDto> TechnicalCareers { get; set; } = new();
    public List<ProfessorCompetencyAssignmentDto> ProfessorAssignments { get; set; } = new();
}

public class TechnicalCareerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}

public class ProfessorCompetencyAssignmentDto
{
    public Guid Id { get; set; }
    public string ProfessorId { get; set; } = string.Empty;
    public string ProfessorName { get; set; } = string.Empty;
    public Guid TechnicalCareerId { get; set; }
    public string TechnicalCareerName { get; set; } = string.Empty;
    public int Year { get; set; }
    public Guid CompetencyId { get; set; }
    public string CompetencyName { get; set; } = string.Empty;
    public string? Status { get; set; }
} 