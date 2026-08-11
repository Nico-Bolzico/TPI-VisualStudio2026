using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class MateriaRepository : IMateriaRepository
    {
        private readonly TPIContext context;

        public MateriaRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Materia materia)
        {
            context.Materias.Add(materia);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var materia = await context.Materias.FindAsync(id);
            if (materia != null)
            {
                context.Materias.Remove(materia);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Materia?> GetAsync(int id)
        {
            return await context.Materias
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Materia>> GetAllAsync()
        {
            return await context.Materias
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Materia materia)
        {
            var existingMateria = await context.Materias.FindAsync(materia.Id);
            if (existingMateria != null)
            {

                existingMateria.SetDescripcion(materia.Descripcion);
                existingMateria.SetHoras(materia.HsSemanales, materia.HsTotales);
                existingMateria.SetIdPlan(materia.IdPlan);

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /*public Task<IEnumerable<Materia>> GetByCriteriaAsync(MateriaCriteria criteria)
        {
            string searchTerm = criteria.Texto.ToLower();

            IEnumerable<Materia> result = materias.Where(m =>
                m.Descripcion.ToLower().Contains(searchTerm)
            ).OrderBy(m => m.Descripcion).ToList();

            return Task.FromResult(result);
        }*/
    }
}
