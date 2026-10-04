using Domain.Model;

namespace Data
{
    public interface IPlanRepository
    {
        Task AddAsync(Plan plan);
        Task<bool> DeleteAsync(int id);
        Task<Plan?> GetAsync(int id);
        Task<IEnumerable<Plan>> GetAllAsync();
        Task<IEnumerable<Plan>> GetByCriteriaAsync(PlanCriteria criteria);
        Task<bool> UpdateAsync(Plan plan);
    }
}
