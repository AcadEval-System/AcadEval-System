using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Repositories;
using AcadEvalSys.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AcadEvalSys.Infrastructure.Repositories;

public class ProfessorCompetencyAssignmentRepository(ApplicationDbContext dbContext) : IProfessorCompetencyAssignmentRepository
{
    public async Task<Guid> CreateAsync(ProfessorCompetencyAssignment assignment)
    {
        var result = dbContext.ProfessorCompetencyAssignments.Add(assignment);
        await dbContext.SaveChangesAsync();
        return result.Entity.Id;
    }

    public async Task CreateMultipleAsync(IEnumerable<ProfessorCompetencyAssignment> assignments)
    {
        await dbContext.ProfessorCompetencyAssignments.AddRangeAsync(assignments);
        await dbContext.SaveChangesAsync();
    }

    

    public async Task UpdateAsync(ProfessorCompetencyAssignment assignment)
    {
        dbContext.ProfessorCompetencyAssignments.Update(assignment);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var assignment = await dbContext.ProfessorCompetencyAssignments.FirstOrDefaultAsync(pca => pca.Id == id);
        if (assignment != null)
        {
            dbContext.ProfessorCompetencyAssignments.Remove(assignment);
            await dbContext.SaveChangesAsync();
        }
    }

    public Task DeleteByEvaluationPeriodIdAsync(Guid evaluationPeriodId)
    {
        throw new NotImplementedException();
    }
} 