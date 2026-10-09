using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ComisionRepository : IComisionRepository
    {
        private readonly TPIContext context;

        public ComisionRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Comision comision)
        {
            context.Comisiones.Add(comision);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var comision = await context.Comisiones.FindAsync(id);
            if (comision != null)
            {
                context.Comisiones.Remove(comision);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Comision?> GetAsync(int id)
        {
            return await context.Comisiones.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Comision>> GetAllAsync()
        {
            return await context.Comisiones
                .OrderBy(c => c.AnioEspecialidad)
                .ThenBy(c => c.Descripcion)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comision>> GetByCriteriaAsync(ComisionCriteria criteria)
        {
            string searchPattern = $"%{criteria.Texto}%";

            return await context.Comisiones
                .Where(c => EF.Functions.Like(c.Descripcion, searchPattern))
                .OrderBy(c => c.AnioEspecialidad)
                .ThenBy(c => c.Descripcion)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Comision comision)
        {
            var existing = await context.Comisiones.FindAsync(comision.Id);
            if (existing != null)
            {
                existing.SetDescripcion(comision.Descripcion);
                existing.SetAnioEspecialidad(comision.AnioEspecialidad);
                existing.SetIdPlan(comision.IdPlan);

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
