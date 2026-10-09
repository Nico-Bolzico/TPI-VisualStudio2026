using Domain.Model;

namespace Data
{
    public interface IEspecialidadRepository
    {
        Task AddAsync(Especialidad especialidad);
        Task<bool> DeleteAsync(int id);
        Task<Especialidad?> GetAsync(int id);
        Task<IEnumerable<Especialidad>> GetAllAsync();
        Task<bool> UpdateAsync(Especialidad especialidad);
        Task<IEnumerable<Especialidad>> GetByCriteriaAsync(EspecialidadCriteria criteria);
    }
}
