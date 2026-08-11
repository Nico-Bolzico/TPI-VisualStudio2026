using DTOs;

namespace Application.Services
{
    public interface IPersonaService
    {
        Task<PersonaDTO> AddAsync(PersonaDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<PersonaDTO?> GetAsync(int id);
        Task<IEnumerable<PersonaDTO>> GetAllAsync();
        Task<bool> UpdateAsync(PersonaDTO dto);
        Task<IEnumerable<PersonaDTO>> GetByCriteriaAsync(PersonaCriteriaDTO criteriaDTO);
    }
}
