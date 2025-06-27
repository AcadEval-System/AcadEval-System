using AcadEvalSys.Domain.Entities;

namespace AcadEvalSys.Domain.Repositories;

public interface IEvaluationPeriodRepository
{
    Task<Guid> CreateEvaluationPeriodAsync(EvaluationPeriod evaluationPeriod);
    Task<EvaluationPeriod?> GetEvaluationPeriodByIdAsync(Guid id);
    Task<IEnumerable<EvaluationPeriod>> GetAllEvaluationPeriodsAsync();
    Task UpdateEvaluationPeriodAsync(EvaluationPeriod evaluationPeriod);
    Task DeleteEvaluationPeriodAsync(Guid id);
    Task<bool> ExistsByTitleAsync(string title);
    Task<IEnumerable<EvaluationPeriod>> GetActiveEvaluationPeriodsAsync();
} 