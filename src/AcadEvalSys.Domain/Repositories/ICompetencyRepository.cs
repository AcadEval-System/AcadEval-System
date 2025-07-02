using AcadEvalSys.Domain.Entities;

namespace AcadEvalSys.Domain.Repositories;

public interface ICompetencyRepository
{
    Task<IEnumerable<Competency>> GetAllCompetenciesAsync();
    Task<Competency?> GetCompetencyByIdAsync(Guid id);
    Task<Guid> CreateCompetencyAsync(Competency competency);
    Task UpdateCompetencyAsync(Competency competency);
    Task DeleteCompetencyAsync(Guid id, string? updatedByUserId = null);
    Task<bool> ExistsByNameAsync(string name);
    Task<bool> ExistsByIdAsync(Guid id);
}