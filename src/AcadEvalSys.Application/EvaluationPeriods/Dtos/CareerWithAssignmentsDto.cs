using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public record CareerWithAssignmentsDto
{
    // Información de la carrera
    public required Guid TechnicalCareerId { get; init; }
    public required string TechnicalCareerName { get; init; }
    
    // Asignaciones organizadas por año
    public Dictionary<CareerYear, List<CompetencyAssignmentDetailDto>> AssignmentsByYear { get; init; } = new();
    
    // Estadísticas de la carrera en este período
    public int TotalAssignments { get; init; }
    public int TotalProfessors { get; init; }
    public int TotalCompetencies { get; init; }
    public IReadOnlyList<CareerYear> ActiveYears { get; init; } = Array.Empty<CareerYear>();
} 