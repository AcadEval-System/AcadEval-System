using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public class SelectedAcademicYears
{
    public Guid TechnicalCareerId { get; set; }
    public List<CareerYear> Year { get; set; } 
}