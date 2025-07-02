using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Repositories;
using AcadEvalSys.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AcadEvalSys.Infrastructure.Repositories;

public class CompetenciesEvaluationInstanceRepository(ApplicationDbContext dbContext) : ICompetenciesEvaluationInstanceRepository
{
    public async Task<Guid> CreateCompetenciesEvaluationInstanceAsync(CompetenciesEvaluationInstance competenciesEvaluationInstance)
    {
        var result = dbContext.CompetenciesEvaluationInstances.Add(competenciesEvaluationInstance);
        await dbContext.SaveChangesAsync();
        return result.Entity.Id;
    }

    public Task<CompetenciesEvaluationInstance?> GetCompetenciesEvaluationInstanceByIdAsync(Guid id)
    {
        var competenciesEvaluationInstance = dbContext.CompetenciesEvaluationInstances
            .Include(ep => ep.TechnicalCareers)
            .Include(ep => ep.ProfessorCompetencyAssignments)
                .ThenInclude(pca => pca.Competency)
            .Include(ep => ep.ProfessorCompetencyAssignments)
                .ThenInclude(pca => pca.Subject)
                    .ThenInclude(s => s.Professor)
                        .ThenInclude(p => p.User)
            .Include(ep => ep.ProfessorCompetencyAssignments)
                .ThenInclude(pca => pca.Subject)
                    .ThenInclude(s => s.TechnicalCareer)
            .Include(ep => ep.StudentEvaluationReports)
            .FirstOrDefaultAsync(ep => ep.Id == id && ep.IsActive);
        return competenciesEvaluationInstance;
    }

    public async Task<IEnumerable<CompetenciesEvaluationInstance>> GetAllCompetenciesEvaluationInstancesAsync()
    {
        var competenciesEvaluationInstances = await dbContext.CompetenciesEvaluationInstances
            .Include(ep => ep.TechnicalCareers)
            .Include(ep => ep.ProfessorCompetencyAssignments)
                .ThenInclude(pca => pca.Competency)
            .Include(ep => ep.ProfessorCompetencyAssignments)
                .ThenInclude(pca => pca.Subject)
                    .ThenInclude(s => s.Professor)
                        .ThenInclude(p => p.User)
            .Include(ep => ep.ProfessorCompetencyAssignments)
                .ThenInclude(pca => pca.Subject)
                    .ThenInclude(s => s.TechnicalCareer)
            .Include(ep => ep.StudentEvaluationReports)
            .Where(ep => ep.IsActive)
            .ToListAsync();
        
        return competenciesEvaluationInstances;
    }

    public async Task UpdateCompetenciesEvaluationInstanceAsync(CompetenciesEvaluationInstance competenciesEvaluationInstance)
    {
        dbContext.CompetenciesEvaluationInstances.Update(competenciesEvaluationInstance);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteCompetenciesEvaluationInstanceAsync(Guid id)
    {
        var competenciesEvaluationInstance = await dbContext.CompetenciesEvaluationInstances.FirstOrDefaultAsync(ep => ep.Id == id);
        if (competenciesEvaluationInstance != null)
        {
            competenciesEvaluationInstance.IsActive = false;
            competenciesEvaluationInstance.UpdatedAt = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsByTitleAsync(string title)
    {
        return await dbContext.CompetenciesEvaluationInstances.AnyAsync(ep => ep.Title == title && ep.IsActive);
    }

    public async Task<IEnumerable<CompetenciesEvaluationInstance>> GetActiveCompetenciesEvaluationInstancesAsync()
    {
        return await dbContext.CompetenciesEvaluationInstances
            .Where(ep => ep.IsActive)
            .ToListAsync();
    }
} 