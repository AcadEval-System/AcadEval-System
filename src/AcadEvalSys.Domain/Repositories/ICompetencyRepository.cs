using AcadEvalSys.Domain.Entities;

namespace AcadEvalSys.Domain.Repositories;

public interface ICompetencyRepository
{
    Task<IEnumerable<Competency>> GetAllCompetenciesAsync();
    Task<Competency?> GetCompetencyByIdAsync(Guid id);
    Task<Guid> CreateCompetencyAsync(Competency competency);
    Task UpdateCompetencyAsync(Competency competency);
    Task DeleteCompetencyAsync(Competency competency);
    Task AddCareerCompetenciesAsync(IEnumerable<CareerCompetencies> careerCompetencies);
}