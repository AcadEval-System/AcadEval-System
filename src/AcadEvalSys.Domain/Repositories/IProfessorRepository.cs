using AcadEvalSys.Domain.Entities;

namespace AcadEvalSys.Domain.Repositories;

public interface IProfessorRepository
{ 
    // Define methods for the repository interface
    Task<Professor?> GetByIdAsync(string id);
    Task<List<Professor>> GetAllAsync();
    Task AddAsync(Professor professor);
    Task UpdateAsync(Professor professor);
    Task DeleteAsync(string id);
}