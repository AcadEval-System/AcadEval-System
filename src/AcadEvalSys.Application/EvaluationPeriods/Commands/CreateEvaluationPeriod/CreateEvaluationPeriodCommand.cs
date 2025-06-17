using AcadEvalSys.Application.EvaluationPeriods.Dtos;
using MediatR;

namespace AcadEvalSys.Application.EvaluationPeriods.Commands.CreateEvaluationPeriod;

public class CreateEvaluationPeriodCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime PeriodFrom { get; set; }
    public DateTime PeriodTo { get; set; }
    public List<ProfessorCompetencyAssignment> ProfessorCompetencyAssignments { get; set; } = [];
    public List<SelectedAcademicYears> SelectedAcademicYears  { get; set; } = new();
} 