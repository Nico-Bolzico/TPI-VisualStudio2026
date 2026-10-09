using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class EspecialidadRepository : IEspecialidadRepository
    {
        private readonly TPIContext context;

        public EspecialidadRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Especialidad especialidad)
        {
            context.Especialidades.Add(especialidad);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var especialidad = await context.Especialidades.FindAsync(id);
            if (especialidad != null)
            {
                context.Especialidades.Remove(especialidad);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Especialidad?> GetAsync(int id)
        {
            return await context.Especialidades.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Especialidad>> GetAllAsync()
        {
            return await context.Especialidades
                .OrderBy(e => e.Descripcion)
                .ToListAsync();
        }

        public async Task<IEnumerable<Especialidad>> GetByCriteriaAsync(EspecialidadCriteria criteria)
        {
            string searchPattern = $"%{criteria.Texto}%";

            return await context.Especialidades
                .Where(e => EF.Functions.Like(e.Descripcion, searchPattern))
                .OrderBy(e => e.Descripcion)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Especialidad especialidad)
        {
            var existing = await context.Especialidades.FindAsync(especialidad.Id);
            if (existing != null)
            {
                existing.SetDescripcion(especialidad.Descripcion);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
