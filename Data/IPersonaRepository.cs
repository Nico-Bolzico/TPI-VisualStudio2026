using Domain.Model;

namespace Data
{
    public interface IPersonaRepository
    {
        Task AddAsync(Persona persona);
        Task<bool> DeleteAsync(int id);
        Task<Persona?> GetAsync(int id);
        Task<IEnumerable<Persona>> GetAllAsync();
        Task<bool> UpdateAsync(Persona persona);
        Task<bool> LegajoExistsAsync(int legajo, int? excludeId = null);
        Task<IEnumerable<Persona>> GetByCriteriaAsync(PersonaCriteria criteria);
    }
}
