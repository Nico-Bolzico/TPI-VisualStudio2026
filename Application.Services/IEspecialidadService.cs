using DTOs;

namespace Application.Services
{
    public interface IEspecialidadService
    {
        Task<EspecialidadDTO> AddAsync(EspecialidadDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<EspecialidadDTO?> GetAsync(int id);
        Task<IEnumerable<EspecialidadDTO>> GetAllAsync();
        Task<IEnumerable<EspecialidadDTO>> GetByCriteriaAsync(EspecialidadCriteriaDTO criteriaDTO);
        Task<bool> UpdateAsync(EspecialidadDTO dto);
    }
}
