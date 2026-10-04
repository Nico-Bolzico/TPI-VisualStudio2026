using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class PlanRepository : IPlanRepository
    {
        private readonly TPIContext context;

        public PlanRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Plan plan)
        {
            context.Planes.Add(plan);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var plan = await context.Planes
                .Include(p => p.Materias)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (plan != null)
            {
                context.Planes.Remove(plan);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Plan?> GetAsync(int id)
        {
            return await context.Planes
                .Include(p => p.Materias)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Plan>> GetAllAsync()
        {
            return await context.Planes
                .Include(p => p.Materias)
                .OrderBy(p => p.Descripcion)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plan>> GetByCriteriaAsync(PlanCriteria criteria)
        {
            string searchPattern = $"%{criteria.Texto}%";

            return await context.Planes
                .Include(p => p.Materias)
                .Where(p => EF.Functions.Like(p.Descripcion, searchPattern))
                .OrderBy(p => p.Descripcion)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Plan plan)
        {
            var existingPlan = await context.Planes
                .Include(p => p.Materias)
                .FirstOrDefaultAsync(p => p.Id == plan.Id);

            if (existingPlan == null)
                return false;

            existingPlan.SetDescripcion(plan.Descripcion);
            existingPlan.SetIdEspecialidad(plan.IdEspecialidad);

            var materiasAEliminar = existingPlan.Materias
                .Where(m => !plan.Materias.Any(pm => pm.Id == m.Id && m.Id != 0))
                .ToList();

            foreach (var m in materiasAEliminar)
            {
                context.Materias.Remove(m);
            }

            foreach (var materia in plan.Materias)
            {
                if (materia.Id == 0)
                {
                    materia.SetIdPlan(existingPlan.Id);
                    existingPlan.AgregarMateria(materia);
                }
                else
                {
                    var existingMateria = existingPlan.Materias.FirstOrDefault(m => m.Id == materia.Id);
                    if (existingMateria != null)
                    {
                        existingMateria.SetDescripcion(materia.Descripcion);
                        existingMateria.SetHoras(materia.HsSemanales, materia.HsTotales);
                        existingMateria.SetIdPlan(existingPlan.Id);
                    }
                }
            }

            await context.SaveChangesAsync();
            return true;
        }
    }
}
