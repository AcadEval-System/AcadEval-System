using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Repositories;
using AcadEvalSys.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AcadEvalSys.Infrastructure.Repositories;

public class CompetencyRepository(ApplicationDbContext dbContext) : ICompetencyRepository
{
    public async Task<IEnumerable<Competency>> GetAllCompetenciesAsync()
    {
        var competencies = await dbContext.Competencies.Where(c => c.IsActive).ToListAsync();
        return competencies;
    }

    public async Task<Competency?> GetCompetencyByIdAsync(Guid id)
    {
        var competency = await dbContext.Competencies.FirstOrDefaultAsync(c => c.Id == id);
        return competency;
    }

    public async Task<Guid> CreateCompetencyAsync(Competency competency)
    {
        var result = dbContext.Competencies.Add(competency);
        await dbContext.SaveChangesAsync();
        return result.Entity.Id;
    }

    public async Task UpdateCompetencyAsync(Competency competency)
    {
        dbContext.Competencies.Update(competency);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteCompetencyAsync(Guid id, string? updatedByUserId = null)
    {
        var competencyToDelete = await dbContext.Competencies.FirstOrDefaultAsync(c => c.Id == id);
        
        if (competencyToDelete == null)
        {
            throw new InvalidOperationException($"Competency with ID {id} was not found.");
        }
        
        competencyToDelete.IsActive = false;
        competencyToDelete.UpdatedAt = DateTime.UtcNow;
        competencyToDelete.UpdatedByUserId = updatedByUserId ?? String.Empty;
        
        await dbContext.SaveChangesAsync();
    }

    public Task<bool> ExistsByNameAsync(string name)
    {
        return dbContext.Competencies.AnyAsync(c => c.Name == name);
    }
}