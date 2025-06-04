using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Repositories;
using AcadEvalSys.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AcadEvalSys.Infrastructure.Repositories;

public class CompetencyRepository(ApplicationDbContext dbContext) : ICompetencyRepository
{
    public async Task<IEnumerable<Competency>> GetAllCompetenciesAsync()
    {
        var competencies = await dbContext.Competencies.ToListAsync();
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

    public async Task DeleteCompetencyAsync(Competency competency)
    {
        dbContext.Remove(competency);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddCareerCompetenciesAsync(IEnumerable<CareerCompetencies> careerCompetencies)
    {
        dbContext.CareerCompetencies.AddRange(careerCompetencies);
        await dbContext.SaveChangesAsync();
    }
}