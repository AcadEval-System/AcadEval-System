using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public record CompetencyAssignmentDetailDto
{
    // Información de la asignación
    public required Guid AssignmentId { get; init; }
    public required CareerYear Year { get; init; }
    
    // Información de la competencia
    public required Guid CompetencyId { get; init; }
    public required string CompetencyName { get; init; }
    public string CompetencyDescription { get; init; } = string.Empty;
    public required CompetencyType CompetencyType { get; init; }
    
    // Información del profesor
    public required string ProfessorId { get; init; }
    public required string ProfessorName { get; init; }
    public string ProfessorEmail { get; init; } = string.Empty;
} 