using DTOs;

namespace Application.Services
{
    public interface ICursoService
    {
        Task<CursoDTO> AddAsync(CursoDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<CursoDTO?> GetAsync(int id);
        Task<IEnumerable<CursoDTO>> GetAllAsync();
        Task<IEnumerable<CursoDTO>> GetByMateriaAsync(int idMateria);
        Task<IEnumerable<CursoDTO>> GetByComisionAsync(int idComision);
        Task<bool> UpdateAsync(CursoDTO dto);
    }
}
