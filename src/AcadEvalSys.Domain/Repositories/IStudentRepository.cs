using AcadEvalSys.Domain.Entities;

namespace AcadEvalSys.Domain.Repositories;

public interface IStudentRepository
{
    Task EnrollStudentInCareerAsync(Student student);
    Task <IEnumerable<Student>> GetStudents();
}