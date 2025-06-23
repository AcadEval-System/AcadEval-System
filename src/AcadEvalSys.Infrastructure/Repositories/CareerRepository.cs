using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Repositories;
using AcadEvalSys.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AcadEvalSys.Infrastructure.Repositories;

public class CareerRepository(ApplicationDbContext dbContext) : ICareerRepository
{
    public async Task<IEnumerable<TechnicalCareer>> GetAllCareersAsync()
    {
        var careers = await dbContext.TechnicalCareers.ToListAsync();
        return careers;
    }

    public async Task<TechnicalCareer?> GetCareerByIdAsync(Guid id)
    {
        var career = await dbContext.TechnicalCareers.FirstOrDefaultAsync(c => c.Id == id);
        return career;
    }

    public async Task<IEnumerable<TechnicalCareer>> GetCareersByIdsAsync(IEnumerable<Guid> ids)
    {
        var careers = await dbContext.TechnicalCareers
            .Where(c => ids.Contains(c.Id))
            .ToListAsync();
        return careers;
    }
}