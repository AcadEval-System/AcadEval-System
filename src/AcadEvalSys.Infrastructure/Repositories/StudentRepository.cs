using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Repositories;
using AcadEvalSys.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AcadEvalSys.Infrastructure.Repositories;

public class StudentRepository(ApplicationDbContext applicationDbContext, UserManager<User> userManager) : IStudentRepository
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
        return await applicationDbContext.Students
            .AnyAsync(s => s.UserId == studentId);
    }

    public async Task<bool> IsActiveAsync(string studentId)
    {
        // Verificar que existe en la tabla Students
        var studentExists = await ExistsAsync(studentId);
        if (!studentExists) return false;

        // Verificar el estado del usuario subyacente
        var user = await userManager.FindByIdAsync(studentId);
        if (user == null) return false;

        // Verificar si el usuario no está bloqueado
        var isLockedOut = await userManager.IsLockedOutAsync(user);
        if (isLockedOut) return false;

        // Verificar si el email está confirmado (opcional, según tus reglas de negocio)
        if (!user.EmailConfirmed) return false;

        return true;
    }

    // Métodos de validación y enrollment
    public async Task<bool> ExistsInCareerAsync(string studentId, Guid technicalCareerId)
    {
        return await applicationDbContext.Students
            .AnyAsync(s => s.UserId == studentId && s.TechnicalCareerId == technicalCareerId);
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