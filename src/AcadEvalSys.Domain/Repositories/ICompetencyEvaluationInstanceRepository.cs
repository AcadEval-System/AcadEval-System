using AcadEvalSys.Domain.Entities;

namespace AcadEvalSys.Domain.Repositories;

public interface ICompetencyEvaluationInstanceRepository
{
    Task<Guid> CreateCompetencyEvaluationInstanceAsync(CompetencyEvaluationInstance competencyEvaluationInstance);
    Task<CompetencyEvaluationInstance?> GetCompetencyEvaluationInstanceByIdAsync(Guid id);
    Task<IEnumerable<CompetencyEvaluationInstance>> GetAllCompetencyEvaluationInstancesAsync();
    Task UpdateCompetencyEvaluationInstanceAsync(CompetencyEvaluationInstance competencyEvaluationInstance);
    Task DeleteCompetencyEvaluationInstanceAsync(Guid id);
    Task<bool> ExistsByTitleAsync(string title);
    Task<IEnumerable<CompetencyEvaluationInstance>> GetActiveCompetencyEvaluationInstancesAsync();
}