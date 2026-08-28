using Domain.Model;

namespace Data
{
    public interface IMateriaRepository
    {
        Task AddAsync(Materia materia);
        Task<bool> DeleteAsync(int id);
        Task<Materia?> GetAsync(int id);
        Task<IEnumerable<Materia>> GetAllAsync();
        Task<bool> UpdateAsync(Materia materia);
        Task<IEnumerable<Materia>> GetByCriteriaAsync(MateriaCriteria criteria);
    }
}
