namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public class CareerSummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public int TotalAssignments { get; set; }
    public int TotalProfessors { get; set; }
    public int TotalCompetencies { get; set; }
} 