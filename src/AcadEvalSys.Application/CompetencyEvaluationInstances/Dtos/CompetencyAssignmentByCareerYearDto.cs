using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Application.CompetenciesEvaluationInstances.Dtos;

public class CompetencyAssignmentByCareerYearDto
{
    public CareerYear Year { get; set; }
    public CompetencyAssignmentDetailDto[] Assignments { get; set; } = [];
} 