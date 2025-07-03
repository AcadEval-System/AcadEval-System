using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Repositories;
using AcadEvalSys.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AcadEvalSys.Infrastructure.Repositories;

public class SubjectRepository(ApplicationDbContext dbContext) : ISubjectRepository
{
    public async Task<Guid> CreateSubjectAsync(Subject subject)
    {
        var result = dbContext.Subjects.Add(subject);
        await dbContext.SaveChangesAsync();
        return result.Entity.Id;
    }

    public async Task<Subject?> GetSubjectByIdAsync(Guid id)
    {
        var subject = await dbContext.Subjects
            .Where(s => s.Id == id && s.IsActive)
            .Include(s => s.TechnicalCareer)
            .Include(s => s.Professor!)
                .ThenInclude(p => p.User)
            .FirstOrDefaultAsync();

        if (subject != null)
        {
            // Cargar StudentSubjects con Students por separado
            subject.StudentSubjects = await dbContext.StudentSubjects
                .Where(ss => ss.SubjectId == id && ss.IsActive)
                .Include(ss => ss.Student!)
                    .ThenInclude(s => s.User)
                .Include(ss => ss.Student!)
                    .ThenInclude(s => s.TechnicalCareer)
                .ToListAsync();
        }

        return subject;
    }

    public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
    {
        var subjects = await dbContext.Subjects
            .Where(s => s.IsActive)
            .Include(s => s.TechnicalCareer)
            .Include(s => s.Professor!)
                .ThenInclude(p => p.User)
            .ToListAsync();

        // Cargar StudentSubjects para todos los subjects
        var subjectIds = subjects.Select(s => s.Id).ToList();
        var allStudentSubjects = await dbContext.StudentSubjects
            .Where(ss => subjectIds.Contains(ss.SubjectId!.Value) && ss.IsActive)
            .Include(ss => ss.Student!)
                .ThenInclude(s => s.User)
            .Include(ss => ss.Student!)
                .ThenInclude(s => s.TechnicalCareer)
            .ToListAsync();

        // Asignar StudentSubjects a cada Subject
        foreach (var subject in subjects)
        {
            subject.StudentSubjects = allStudentSubjects
                .Where(ss => ss.SubjectId == subject.Id)
                .ToList();
        }

        return subjects;
    }

    public async Task<IEnumerable<Subject>> GetSubjectsByCareerAsync(Guid technicalCareerId)
    {
        return await dbContext.Subjects
            .Include(s => s.TechnicalCareer)
            .Include(s => s.Professor)
                .ThenInclude(p => p!.User)
            .Where(s => s.TechnicalCareerId == technicalCareerId && s.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<Subject>> GetSubjectsByProfessorAsync(string professorId)
    {
        return await dbContext.Subjects
            .Include(s => s.TechnicalCareer)
            .Include(s => s.Professor)
                .ThenInclude(p => p!.User)
            .Where(s => s.ProfessorId == professorId && s.IsActive)
            .ToListAsync();
    }

    public async Task UpdateSubjectAsync(Subject subject)
    {
        dbContext.Subjects.Update(subject);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteSubjectAsync(Guid id, string? updatedByUserId = null)
    {
        var subject = await dbContext.Subjects.FirstOrDefaultAsync(s => s.Id == id);
        if (subject != null)
        {
            subject.IsActive = false;
            subject.UpdatedAt = DateTime.UtcNow;
            subject.UpdatedByUserId = updatedByUserId;
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsByNameAndCareerAsync(string name, Guid technicalCareerId)
    {
        return await dbContext.Subjects
            .AnyAsync(s => s.Name == name && s.TechnicalCareerId == technicalCareerId && s.IsActive);
    }

    public async Task<bool> TechnicalCareerExistsAsync(Guid technicalCareerId)
    {
        return await dbContext.TechnicalCareers
            .AnyAsync(tc => tc.Id == technicalCareerId && tc.IsActive);
    }

    public async Task<bool> ProfessorExistsAsync(string professorId)
    {
        return await dbContext.Professors
            .AnyAsync(p => p.UserId == professorId);
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        return await dbContext.Subjects
            .AnyAsync(s => s.Id == id && s.IsActive);
    }

    // Métodos para estudiantes
    public async Task<bool> StudentExistsAsync(string studentId)
    {
        return await dbContext.Students
            .AnyAsync(s => s.UserId == studentId);
    }

    public async Task<bool> IsStudentEnrolledInSubjectAsync(string studentId, Guid subjectId)
    {
        return await dbContext.StudentSubjects
            .AnyAsync(ss => ss.StudentId == studentId && ss.SubjectId == subjectId && ss.IsActive);
    }

    public async Task EnrollStudentInSubjectAsync(string studentId, Guid subjectId)
    {
        var studentSubject = new StudentSubject
        {
            StudentId = studentId,
            SubjectId = subjectId
        };

        dbContext.StudentSubjects.Add(studentSubject);
        await dbContext.SaveChangesAsync();
    }

    public async Task UnenrollStudentFromSubjectAsync(string studentId, Guid subjectId)
    {
        var studentSubject = await dbContext.StudentSubjects
            .FirstOrDefaultAsync(ss => ss.StudentId == studentId && ss.SubjectId == subjectId && ss.IsActive);

        if (studentSubject != null)
        {
            studentSubject.IsActive = false;
            studentSubject.UpdatedAt = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Student>> GetStudentsInSubjectAsync(Guid subjectId)
    {
        return await dbContext.StudentSubjects
            .Where(ss => ss.SubjectId == subjectId && ss.IsActive)
            .Include(ss => ss.Student)
                .ThenInclude(s => s!.User)
            .Include(ss => ss.Student)
                .ThenInclude(s => s!.TechnicalCareer)
            .Select(ss => ss.Student!)
            .ToListAsync();
    }

    // Métodos para profesores
    public async Task AssignProfessorToSubjectAsync(Guid subjectId, string professorId)
    {
        var subject = await dbContext.Subjects.FirstOrDefaultAsync(s => s.Id == subjectId && s.IsActive);
        if (subject != null)
        {
            subject.ProfessorId = professorId;
            subject.UpdatedAt = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task RemoveProfessorFromSubjectAsync(Guid subjectId)
    {
        var subject = await dbContext.Subjects.FirstOrDefaultAsync(s => s.Id == subjectId && s.IsActive);
        if (subject != null)
        {
            subject.ProfessorId = null;
            subject.UpdatedAt = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
        }
    }
}