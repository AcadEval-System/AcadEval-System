using AcadEvalSys.Domain.Entities;

namespace AcadEvalSys.Domain.Repositories;

public interface ICompetenciesEvaluationInstanceRepository
{
    Task<Guid> CreateCompetenciesEvaluationInstanceAsync(CompetenciesEvaluationInstance competenciesEvaluationInstance);
    Task<CompetenciesEvaluationInstance?> GetCompetenciesEvaluationInstanceByIdAsync(Guid id);
    Task<IEnumerable<CompetenciesEvaluationInstance>> GetAllCompetenciesEvaluationInstancesAsync();
    Task UpdateCompetenciesEvaluationInstanceAsync(CompetenciesEvaluationInstance competenciesEvaluationInstance);
    Task DeleteCompetenciesEvaluationInstanceAsync(Guid id);
    Task<bool> ExistsByTitleAsync(string title);
    Task<IEnumerable<CompetenciesEvaluationInstance>> GetActiveCompetenciesEvaluationInstancesAsync();
} 