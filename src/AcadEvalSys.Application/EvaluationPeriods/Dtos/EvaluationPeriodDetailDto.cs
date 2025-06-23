namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public class EvaluationPeriodDetailDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime PeriodFrom { get; set; }
    public DateTime PeriodTo { get; set; }
    
    public List<CareerWithAssignmentsDto> CareerAssignments { get; set; } = new List<CareerWithAssignmentsDto>();
    
}