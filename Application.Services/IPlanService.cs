using DTOs;

namespace Application.Services
{
    public interface IPlanService
    {
        Task<PlanDTO> AddAsync(PlanDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<PlanDTO?> GetAsync(int id);
        Task<IEnumerable<PlanDTO>> GetAllAsync();
        Task<IEnumerable<PlanDTO>> GetByCriteriaAsync(PlanCriteriaDTO criteriaDTO);
        Task<bool> UpdateAsync(PlanDTO dto);
    }
}
