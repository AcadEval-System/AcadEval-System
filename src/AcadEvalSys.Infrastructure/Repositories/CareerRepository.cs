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

    public Task<TechnicalCareer?> GetCareerByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}