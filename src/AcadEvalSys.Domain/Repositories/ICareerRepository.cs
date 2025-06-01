using AcadEvalSys.Domain.Entities;

namespace AcadEvalSys.Domain.Repositories;

public interface ICareerRepository
{
    Task<IEnumerable<TechnicalCareer>> GetAllCareersAsync();
    Task<TechnicalCareer?> GetCareerByIdAsync(Guid id);
}