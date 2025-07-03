using AcadEvalSys.Domain.Entities;

namespace AcadEvalSys.Domain.Repositories;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetStudents();
    Task<Student?> GetStudentByIdAsync(string studentId);
    Task<bool> ExistsAsync(string studentId);
    Task<bool> IsActiveAsync(string studentId);

    Task<bool> ExistsInCareerAsync(string studentId, Guid technicalCareerId);
    Task<bool> IsEnrolledInSubjectAsync(string studentId, Guid subjectId);
    Task EnrollInSubjectAsync(string studentId, Guid subjectId);
    Task UnenrollFromSubjectAsync(string studentId, Guid subjectId);
    Task<IEnumerable<Student>> GetStudentsInSubjectAsync(Guid subjectId);
    Task<IEnumerable<Subject>> GetSubjectsByStudentAsync(string studentId);
}