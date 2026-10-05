using Domain.Model;
using Data;
using DTOs;

namespace Application.Services
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository planRepository;

        public PlanService(IPlanRepository planRepository)
        {
            this.planRepository = planRepository;
        }

        public async Task<PlanDTO> AddAsync(PlanDTO dto)
        {
            var plan = new Plan(0, dto.Descripcion, dto.IdEspecialidad);

            if (dto.Materias != null)
            {
                foreach (var m in dto.Materias)
                {
                    plan.AgregarMateria(Materia.NuevaParaPlan(m.Descripcion, m.HsSemanales, m.HsTotales));
                }
            }

            await planRepository.AddAsync(plan);

            return ToDto(plan);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await planRepository.DeleteAsync(id);
        }

        public async Task<PlanDTO?> GetAsync(int id)
        {
            var plan = await planRepository.GetAsync(id);
            return plan == null ? null : ToDto(plan);
        }

        public async Task<IEnumerable<PlanDTO>> GetAllAsync()
        {
            var planes = await planRepository.GetAllAsync();
            return planes.Select(ToDto).ToList();
        }

        public async Task<IEnumerable<PlanDTO>> GetByCriteriaAsync(PlanCriteriaDTO criteriaDTO)
        {
            var criteria = new PlanCriteria(criteriaDTO.Texto);
            var planes = await planRepository.GetByCriteriaAsync(criteria);
            return planes.Select(ToDto).ToList();
        }

        public async Task<bool> UpdateAsync(PlanDTO dto)
        {
            var materias = dto.Materias?.Select(m =>
                new Materia(m.Id, m.Descripcion, m.HsSemanales, m.HsTotales, dto.Id))
                .ToList() ?? new List<Materia>();

            var plan = new Plan(dto.Id, dto.Descripcion, dto.IdEspecialidad, materias);

            return await planRepository.UpdateAsync(plan);
        }

        private static PlanDTO ToDto(Plan plan)
        {
            return new PlanDTO
            {
                Id = plan.Id,
                Descripcion = plan.Descripcion,
                IdEspecialidad = plan.IdEspecialidad,
                Materias = plan.Materias.Select(m => new MateriaDTO
                {
                    Id = m.Id,
                    Descripcion = m.Descripcion,
                    HsSemanales = m.HsSemanales,
                    HsTotales = m.HsTotales,
                    IdPlan = m.IdPlan
                }).ToList()
            };
        }
    }
}
