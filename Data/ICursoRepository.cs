using Domain.Model;

namespace Data
{
    public interface ICursoRepository
    {
        Task AddAsync(Curso curso);
        Task<bool> DeleteAsync(int id);
        Task<Curso?> GetAsync(int id);
        Task<IEnumerable<Curso>> GetAllAsync();
        Task<IEnumerable<Curso>> GetByMateriaAsync(int idMateria);
        Task<IEnumerable<Curso>> GetByComisionAsync(int idComision);
        Task<bool> UpdateAsync(Curso curso);
    }
}
