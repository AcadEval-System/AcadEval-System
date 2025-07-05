using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Repositories;
using AcadEvalSys.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AcadEvalSys.Infrastructure.Repositories;

public class StudentRepository(ApplicationDbContext applicationDbContext) : IStudentRepository
{
    public async Task<IEnumerable<Student>> GetStudents()
    {
        return await applicationDbContext.Students
            .Include(s => s.User)
            .Include(s => s.TechnicalCareer)
            .ToListAsync();
    }

    public async Task<Student?> GetStudentByIdAsync(string studentId)
    {
        return await applicationDbContext.Students
            .Include(s => s.User)
            .Include(s => s.TechnicalCareer)
            .Include(s => s.StudentSubjects!)
                .ThenInclude(ss => ss.Subject!)
                    .ThenInclude(s => s.TechnicalCareer)
            .FirstOrDefaultAsync(s => s.UserId == studentId);
    }

    public async Task<bool> ExistsAsync(string studentId)
    {
        var student = await GetStudentByIdAsync(studentId);
        return student != null;
    }

    public async Task CreateStudentAsync(Student student)
    {
        applicationDbContext.Students.Add(student);
        await applicationDbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsInCareerAsync(string studentId, Guid technicalCareerId)
    {
        var student = await GetStudentByIdAsync(studentId);
        return student != null && student.TechnicalCareerId == technicalCareerId;
    }


    public async Task<bool> IsEnrolledInSubjectAsync(string studentId, Guid subjectId)
    {
        return await applicationDbContext.StudentSubjects
            .AnyAsync(ss => ss.StudentId == studentId && ss.SubjectId == subjectId && ss.IsActive);
    }

    public async Task EnrollInSubjectAsync(string studentId, Guid subjectId)
    {
        var studentSubject = new StudentSubject
        {
            StudentId = studentId,
            SubjectId = subjectId
        };

        applicationDbContext.StudentSubjects.Add(studentSubject);
        await applicationDbContext.SaveChangesAsync();
    }

    public async Task UnenrollFromSubjectAsync(string studentId, Guid subjectId)
    {
        var studentSubject = await applicationDbContext.StudentSubjects
            .FirstOrDefaultAsync(ss => ss.StudentId == studentId && ss.SubjectId == subjectId && ss.IsActive);

        if (studentSubject != null)
        {
            studentSubject.IsActive = false;
            studentSubject.UpdatedAt = DateTime.UtcNow;
            await applicationDbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Student>> GetStudentsInSubjectAsync(Guid subjectId)
    {
        return await applicationDbContext.StudentSubjects
            .Where(ss => ss.SubjectId == subjectId && ss.IsActive)
            .Include(ss => ss.Student!)
                .ThenInclude(s => s.User)
            .Include(ss => ss.Student!)
                .ThenInclude(s => s.TechnicalCareer)
            .Select(ss => ss.Student!)
            .ToListAsync();
    }

    public async Task<IEnumerable<Subject>> GetSubjectsByStudentAsync(string studentId)
    {
        return await applicationDbContext.StudentSubjects
            .Where(ss => ss.StudentId == studentId && ss.IsActive)
            .Include(ss => ss.Subject!)
                .ThenInclude(s => s.TechnicalCareer)
            .Include(ss => ss.Subject!)
                .ThenInclude(s => s.Professor!)
                    .ThenInclude(p => p.User)
            .Select(ss => ss.Subject!)
            .ToListAsync();
    }
}