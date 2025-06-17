using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public class ProfessorCompetencyAssignment
{
    public string ProfessorId { get; set; } = string.Empty;
    public Guid TechnicalCareerId { get; set; }
    public CareerYear Year { get; set; }
    public Guid CompetencyId { get; set; }
}