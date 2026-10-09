using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class CursoRepository : ICursoRepository
    {
        private readonly TPIContext context;

        public CursoRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Curso curso)
        {
            context.Cursos.Add(curso);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var curso = await context.Cursos.FindAsync(id);
            if (curso != null)
            {
                context.Cursos.Remove(curso);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Curso?> GetAsync(int id)
        {
            return await context.Cursos.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Curso>> GetAllAsync()
        {
            return await context.Cursos
                .OrderByDescending(c => c.AnioCalendario)
                .ThenBy(c => c.IdMateria)
                .ToListAsync();
        }

        public async Task<IEnumerable<Curso>> GetByMateriaAsync(int idMateria)
        {
            return await context.Cursos
                .Where(c => c.IdMateria == idMateria)
                .OrderByDescending(c => c.AnioCalendario)
                .ToListAsync();
        }

        public async Task<IEnumerable<Curso>> GetByComisionAsync(int idComision)
        {
            return await context.Cursos
                .Where(c => c.IdComision == idComision)
                .OrderByDescending(c => c.AnioCalendario)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Curso curso)
        {
            var existing = await context.Cursos.FindAsync(curso.Id);
            if (existing != null)
            {
                existing.SetIdMateria(curso.IdMateria);
                existing.SetIdComision(curso.IdComision);
                existing.SetAnioCalendario(curso.AnioCalendario);
                existing.SetCupo(curso.Cupo);

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
