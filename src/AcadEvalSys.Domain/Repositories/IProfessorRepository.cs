using AcadEvalSys.Domain.Entities;

namespace AcadEvalSys.Domain.Repositories;

public interface IProfessorRepository
{
    Task<Professor?> GetProfessorByIdAsync(string professorId);
    Task<bool> ExistsAsync(string professorId);
    Task<bool> IsActiveAsync(string professorId);
    Task<IEnumerable<Professor>> GetAllProfessorsAsync();
    Task<IEnumerable<Subject>> GetSubjectsByProfessorAsync(string professorId);
}