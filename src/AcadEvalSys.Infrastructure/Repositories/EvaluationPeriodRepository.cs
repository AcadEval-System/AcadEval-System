using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Repositories;
using AcadEvalSys.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AcadEvalSys.Infrastructure.Repositories;

public class EvaluationPeriodRepository(ApplicationDbContext dbContext) : IEvaluationPeriodRepository
{
    public async Task<Guid> CreateEvaluationPeriodAsync(EvaluationPeriod evaluationPeriod)
    {
        var result = dbContext.EvaluationPeriods.Add(evaluationPeriod);
        await dbContext.SaveChangesAsync();
        return result.Entity.Id;
    }

    public Task<EvaluationPeriod?> GetEvaluationPeriodByIdAsync(Guid id)
    {
        var evaluationPeriod = dbContext.EvaluationPeriods
            .Include(ep => ep.TechnicalCareers)
            .Include(ep => ep.ProfessorCompetencyAssignments)
                .ThenInclude(pca => pca.Competency)
            .Include(ep => ep.ProfessorCompetencyAssignments)
                .ThenInclude(pca => pca.Professor)
                    .ThenInclude(p => p.User)
            .Include(ep => ep.ProfessorCompetencyAssignments)
                .ThenInclude(pca => pca.TechnicalCareer)
            .Include(ep => ep.StudentEvaluationReports)
            .FirstOrDefaultAsync(ep => ep.Id == id && ep.IsActive);
        return evaluationPeriod;
    }

    public Task<IEnumerable<EvaluationPeriod>> GetAllEvaluationPeriodsAsync()
    {
        throw new NotImplementedException();
    }


    public async Task UpdateEvaluationPeriodAsync(EvaluationPeriod evaluationPeriod)
    {
        dbContext.EvaluationPeriods.Update(evaluationPeriod);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteEvaluationPeriodAsync(Guid id)
    {
        var evaluationPeriod = await dbContext.EvaluationPeriods.FirstOrDefaultAsync(ep => ep.Id == id);
        if (evaluationPeriod != null)
        {
            evaluationPeriod.IsActive = false;
            evaluationPeriod.UpdatedAt = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsByTitleAsync(string title)
    {
        return await dbContext.EvaluationPeriods.AnyAsync(ep => ep.Title == title && ep.IsActive);
    }

    public Task<IEnumerable<EvaluationPeriod>> GetActiveEvaluationPeriodsAsync()
    {
        throw new NotImplementedException();
    }
} 