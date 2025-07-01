using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Repositories;
using AcadEvalSys.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AcadEvalSys.Infrastructure.Repositories;

public class ProfessorRepository(ApplicationDbContext dbContext) : IProfessorRepository
{
    public Task<Professor?> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Professor>> GetAllAsync()
    {
        return dbContext.Professors
            .Include(p => p.User)
            .Include(p => p.Subjects)
            .Include(p => p.ProfessorCompetencyAssignments)
            .ToListAsync();
    }

    public Task AddAsync(Professor professor)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Professor professor)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }
}