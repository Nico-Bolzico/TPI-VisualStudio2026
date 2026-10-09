using Domain.Model;

namespace Data
{
    public interface IComisionRepository
    {
        Task AddAsync(Comision comision);
        Task<bool> DeleteAsync(int id);
        Task<Comision?> GetAsync(int id);
        Task<IEnumerable<Comision>> GetAllAsync();
        Task<bool> UpdateAsync(Comision comision);
        Task<IEnumerable<Comision>> GetByCriteriaAsync(ComisionCriteria criteria);
    }
}
