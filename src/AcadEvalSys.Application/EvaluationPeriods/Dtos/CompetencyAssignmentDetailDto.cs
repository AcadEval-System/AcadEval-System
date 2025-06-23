using AcadEvalSys.Domain.Enums;

namespace AcadEvalSys.Application.EvaluationPeriods.Dtos;

public class CompetencyAssignmentDetailDto
{
    // Información de la asignación
    public Guid AssignmentId { get; set; }
    public CareerYear Year { get; set; }
    
    // Información de la carrera
    public Guid TechnicalCareerId { get; set; }
    public string TechnicalCareerName { get; set; } = string.Empty;
    
    // Información de la competencia
    public Guid CompetencyId { get; set; }
    public string CompetencyName { get; set; } = string.Empty;
    public string CompetencyDescription { get; set; } = string.Empty;
    public CompetencyType CompetencyType { get; set; }
    
    // Información del profesor
    public string ProfessorId { get; set; } = string.Empty;
    public string ProfessorName { get; set; } = string.Empty;
    public string ProfessorEmail { get; set; } = string.Empty;
} 