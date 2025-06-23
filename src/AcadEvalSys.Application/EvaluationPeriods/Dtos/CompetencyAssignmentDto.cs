namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public class CompetencyAssignmentDto
{
    public Guid CompetencyId { get; set; }
    public string ProfessorId { get; set; } = string.Empty;
}