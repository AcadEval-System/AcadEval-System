using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public class CompetencyAssignmentByCareerYearDto
{
    public CareerYear Year { get; set; }
    public CompetencyAssignmentDetailDto[] Assignments { get; set; } = [];
}