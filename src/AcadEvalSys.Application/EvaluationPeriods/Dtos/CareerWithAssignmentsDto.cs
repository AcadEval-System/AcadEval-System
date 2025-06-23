using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public class CareerWithAssignmentsDto
{
    // Información de la carrera
    public Guid TechnicalCareerId { get; set; }
    public string TechnicalCareerName { get; set; } = string.Empty;
    
    // Asignaciones organizadas por año
    public Dictionary<CareerYear, List<CompetencyAssignmentDetailDto>> AssignmentsByYear { get; set; } = new();
    
    // Estadísticas de la carrera en este período
    public int TotalAssignments { get; set; }
    public int TotalProfessors { get; set; }
    public int TotalCompetencies { get; set; }
    public List<CareerYear> ActiveYears { get; set; } = new();
} 