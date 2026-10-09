using Domain.Model;

namespace Data
{
    public interface IDocenteCursoRepository
    {
        Task AddAsync(DocenteCurso docenteCurso);
        Task<bool> DeleteAsync(int id);
        Task<DocenteCurso?> GetAsync(int id);
        Task<IEnumerable<DocenteCurso>> GetAllAsync();
        Task<IEnumerable<DocenteCurso>> GetByCursoAsync(int idCurso);
        Task<IEnumerable<DocenteCurso>> GetByDocenteAsync(int idDocente);
        Task<bool> UpdateAsync(DocenteCurso docenteCurso);
    }
}
