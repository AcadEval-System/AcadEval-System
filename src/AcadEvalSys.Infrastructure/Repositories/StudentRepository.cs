using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Repositories;
using AcadEvalSys.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Infrastructure.Repositories;

public class StudentRepository(ApplicationDbContext applicationDbContext) : IStudentRepository
{
    public async Task EnrollStudentInCareerAsync(Student student)
    {
        await applicationDbContext.Students.AddAsync(student);
        await applicationDbContext.SaveChangesAsync();   
    }
    
    public async Task<IEnumerable<Student>> GetStudents()
    {
        return await applicationDbContext.Students.
            Include(s => s.User)
            .ToListAsync();
    }
    
    
}