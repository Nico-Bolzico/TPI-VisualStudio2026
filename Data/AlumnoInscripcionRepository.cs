using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AlumnoInscripcionRepository : IAlumnoInscripcionRepository
    {
        private readonly TPIContext context;

        public AlumnoInscripcionRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(AlumnoInscripcion inscripcion)
        {
            context.AlumnosInscripciones.Add(inscripcion);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var inscripcion = await context.AlumnosInscripciones.FindAsync(id);
            if (inscripcion != null)
            {
                context.AlumnosInscripciones.Remove(inscripcion);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<AlumnoInscripcion?> GetAsync(int id)
        {
            return await context.AlumnosInscripciones.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<AlumnoInscripcion>> GetAllAsync()
        {
            return await context.AlumnosInscripciones
                .OrderBy(i => i.IdCurso)
                .ThenBy(i => i.IdAlumno)
                .ToListAsync();
        }

        public async Task<IEnumerable<AlumnoInscripcion>> GetByCursoAsync(int idCurso)
        {
            return await context.AlumnosInscripciones
                .Where(i => i.IdCurso == idCurso)
                .OrderBy(i => i.IdAlumno)
                .ToListAsync();
        }

        public async Task<IEnumerable<AlumnoInscripcion>> GetByAlumnoAsync(int idAlumno)
        {
            return await context.AlumnosInscripciones
                .Where(i => i.IdAlumno == idAlumno)
                .OrderBy(i => i.IdCurso)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(AlumnoInscripcion inscripcion)
        {
            var existing = await context.AlumnosInscripciones.FindAsync(inscripcion.Id);
            if (existing != null)
            {
                existing.SetIdAlumno(inscripcion.IdAlumno);
                existing.SetIdCurso(inscripcion.IdCurso);
                existing.SetCondicion(inscripcion.Condicion);
                existing.SetNota(inscripcion.Nota);

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<int> ContarInscriptosAsync(int idCurso)
        {
            return await context.AlumnosInscripciones.CountAsync(i => i.IdCurso == idCurso);
        }

        public async Task<bool> YaEstaInscriptoAsync(int idAlumno, int idCurso)
        {
            return await context.AlumnosInscripciones.AnyAsync(i => i.IdAlumno == idAlumno && i.IdCurso == idCurso);
        }
    }
}
