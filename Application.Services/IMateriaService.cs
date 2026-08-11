using DTOs;

namespace Application.Services
{
    public interface IMateriaService
    {
        Task<MateriaDTO> AddAsync(MateriaDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<MateriaDTO?> GetAsync(int id);
        Task<IEnumerable<MateriaDTO>> GetAllAsync();
        Task<bool> UpdateAsync(MateriaDTO dto);
        Task<IEnumerable<MateriaDTO>> GetByCriteriaAsync(MateriaCriteriaDTO criteriaDTO);
    }
}
