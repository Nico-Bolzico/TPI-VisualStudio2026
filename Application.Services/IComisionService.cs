using DTOs;

namespace Application.Services
{
    public interface IComisionService
    {
        Task<ComisionDTO> AddAsync(ComisionDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<ComisionDTO?> GetAsync(int id);
        Task<IEnumerable<ComisionDTO>> GetAllAsync();
        Task<IEnumerable<ComisionDTO>> GetByCriteriaAsync(ComisionCriteriaDTO criteriaDTO);
        Task<bool> UpdateAsync(ComisionDTO dto);
    }
}
