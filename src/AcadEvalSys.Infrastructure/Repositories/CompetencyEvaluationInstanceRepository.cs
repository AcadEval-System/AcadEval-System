using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Repositories;
using AcadEvalSys.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AcadEvalSys.Infrastructure.Repositories;

public class CompetencyEvaluationInstanceRepository(ApplicationDbContext dbContext) : ICompetencyEvaluationInstanceRepository
{
    public async Task<Guid> CreateCompetencyEvaluationInstanceAsync(CompetencyEvaluationInstance competencyEvaluationInstance)
    {
        var result = dbContext.CompetencyEvaluationInstances.Add(competencyEvaluationInstance);
        await dbContext.SaveChangesAsync();
        return result.Entity.Id;
    }

    public Task<CompetencyEvaluationInstance?> GetCompetencyEvaluationInstanceByIdAsync(Guid id)
    {
        var competencyEvaluationInstance = dbContext.CompetencyEvaluationInstances
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
        return competencyEvaluationInstance;
    }

    public async Task<IEnumerable<CompetencyEvaluationInstance>> GetAllCompetencyEvaluationInstancesAsync()
    {
        var competencyEvaluationInstances = await dbContext.CompetencyEvaluationInstances
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

        return competencyEvaluationInstances;
    }

    public async Task UpdateCompetencyEvaluationInstanceAsync(CompetencyEvaluationInstance competencyEvaluationInstance)
    {
        dbContext.CompetencyEvaluationInstances.Update(competencyEvaluationInstance);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteCompetencyEvaluationInstanceAsync(Guid id)
    {
        var competencyEvaluationInstance = await dbContext.CompetencyEvaluationInstances.FirstOrDefaultAsync(ep => ep.Id == id);
        if (competencyEvaluationInstance != null)
        {
            competencyEvaluationInstance.IsActive = false;
            competencyEvaluationInstance.UpdatedAt = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsByTitleAsync(string title)
    {
        return await dbContext.CompetencyEvaluationInstances.AnyAsync(ep => ep.Title == title && ep.IsActive);
    }

    public async Task<IEnumerable<CompetencyEvaluationInstance>> GetActiveCompetencyEvaluationInstancesAsync()
    {
        return await dbContext.CompetencyEvaluationInstances
            .Where(ep => ep.IsActive)
            .ToListAsync();
    }
}