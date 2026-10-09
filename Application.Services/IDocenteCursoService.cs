using DTOs;

namespace Application.Services
{
    public interface IDocenteCursoService
    {
        Task<DocenteCursoDTO> AddAsync(DocenteCursoDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<DocenteCursoDTO?> GetAsync(int id);
        Task<IEnumerable<DocenteCursoDTO>> GetAllAsync();
        Task<IEnumerable<DocenteCursoDTO>> GetByCursoAsync(int idCurso);
        Task<IEnumerable<DocenteCursoDTO>> GetByDocenteAsync(int idDocente);
        Task<bool> UpdateAsync(DocenteCursoDTO dto);
    }
}
