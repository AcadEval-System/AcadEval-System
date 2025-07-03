using AcadEvalSys.Domain.Entities;

namespace AcadEvalSys.Domain.Repositories;

public interface ISubjectRepository
{
    Task<Guid> CreateSubjectAsync(Subject subject);
    Task<Subject?> GetSubjectByIdAsync(Guid id);
    Task<IEnumerable<Subject>> GetAllSubjectsAsync();
    Task<IEnumerable<Subject>> GetSubjectsByCareerAsync(Guid technicalCareerId);
    Task<IEnumerable<Subject>> GetSubjectsByProfessorAsync(string professorId);
    Task UpdateSubjectAsync(Subject subject);
    Task DeleteSubjectAsync(Subject subject);
    Task<bool> ExistsByNameAndCareerAsync(string name, Guid technicalCareerId);
    Task<bool> TechnicalCareerExistsAsync(Guid technicalCareerId);
    Task<bool> ProfessorExistsAsync(string professorId);
    Task<bool> ExistsByIdAsync(Guid id);


    Task<bool> StudentExistsAsync(string studentId, Guid technicalCareerId);
    Task<bool> IsStudentEnrolledInSubjectAsync(string studentId, Guid subjectId);
    Task EnrollStudentInSubjectAsync(string studentId, Guid subjectId);
    Task UnenrollStudentFromSubjectAsync(string studentId, Guid subjectId);
    Task<IEnumerable<Student>> GetStudentsInSubjectAsync(Guid subjectId);

    // Métodos para profesores
    Task AssignProfessorToSubjectAsync(Guid subjectId, string professorId);
    Task RemoveProfessorFromSubjectAsync(Guid subjectId);
}