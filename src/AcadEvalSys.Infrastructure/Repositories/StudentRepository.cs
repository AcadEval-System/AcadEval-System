using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Repositories;
using AcadEvalSys.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Infrastructure.Repositories;

public class StudentRepository(ApplicationDbContext applicationDbContext) : IStudentRepository
{
    public async Task EnrollStudentInCareerAsync(Student student)
    {
        await applicationDbContext.Students.AddAsync(student);
        await applicationDbContext.SaveChangesAsync();   
    }
}