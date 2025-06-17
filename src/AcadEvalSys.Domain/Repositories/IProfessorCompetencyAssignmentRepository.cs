using AcadEvalSys.Domain.Entities;

namespace AcadEvalSys.Domain.Repositories;

public interface IProfessorCompetencyAssignmentRepository
{
    Task<Guid> CreateAsync(ProfessorCompetencyAssignment assignment);
    Task CreateMultipleAsync(IEnumerable<ProfessorCompetencyAssignment> assignments);
    Task UpdateAsync(ProfessorCompetencyAssignment assignment);
    Task DeleteAsync(Guid id);
    Task DeleteByEvaluationPeriodIdAsync(Guid evaluationPeriodId);
} 