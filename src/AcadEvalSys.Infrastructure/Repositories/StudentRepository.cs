using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Repositories;
using AcadEvalSys.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Infrastructure.Repositories;

public class StudentRepository(ApplicationDbContext applicationDbContext) : IStudentRepository
{
    public async Task<IEnumerable<Student>> GetStudents()
    {
        return await applicationDbContext.Students.
            Include(s => s.User)
            .ToListAsync();
    }
}