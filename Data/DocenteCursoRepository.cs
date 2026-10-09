using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DocenteCursoRepository : IDocenteCursoRepository
    {
        private readonly TPIContext context;

        public DocenteCursoRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(DocenteCurso docenteCurso)
        {
            context.DocentesCursos.Add(docenteCurso);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await context.DocentesCursos.FindAsync(id);
            if (entity != null)
            {
                context.DocentesCursos.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<DocenteCurso?> GetAsync(int id)
        {
            return await context.DocentesCursos.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<DocenteCurso>> GetAllAsync()
        {
            return await context.DocentesCursos
                .OrderBy(d => d.IdCurso)
                .ThenBy(d => d.IdDocente)
                .ToListAsync();
        }

        public async Task<IEnumerable<DocenteCurso>> GetByCursoAsync(int idCurso)
        {
            return await context.DocentesCursos
                .Where(d => d.IdCurso == idCurso)
                .OrderBy(d => d.Cargo)
                .ToListAsync();
        }

        public async Task<IEnumerable<DocenteCurso>> GetByDocenteAsync(int idDocente)
        {
            return await context.DocentesCursos
                .Where(d => d.IdDocente == idDocente)
                .OrderBy(d => d.IdCurso)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(DocenteCurso docenteCurso)
        {
            var existing = await context.DocentesCursos.FindAsync(docenteCurso.Id);
            if (existing != null)
            {
                existing.SetIdCurso(docenteCurso.IdCurso);
                existing.SetIdDocente(docenteCurso.IdDocente);
                existing.SetCargo(docenteCurso.Cargo);

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
