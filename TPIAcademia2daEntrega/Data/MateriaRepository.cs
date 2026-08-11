using Domain.Model;

namespace Data
{
    public class MateriaRepository : IMateriaRepository
    {
        private static readonly List<Materia> materias = new List<Materia>();
        private static int nextId = 1;

        public Task AddAsync(Materia materia)
        {
            // Simular auto-increment de ID
            materia.SetId(nextId);
            nextId++;

            materias.Add(materia);
            return Task.CompletedTask;
        }

        public Task<bool> DeleteAsync(int id)
        {
            var materia = materias.FirstOrDefault(m => m.Id == id);
            if (materia != null)
            {
                materias.Remove(materia);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<Materia?> GetAsync(int id)
        {
            return Task.FromResult(materias.FirstOrDefault(m => m.Id == id));
        }

        public Task<IEnumerable<Materia>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Materia>>(materias.ToList());
        }

        public Task<bool> UpdateAsync(Materia materia)
        {
            var existing = materias.FirstOrDefault(m => m.Id == materia.Id);
            if (existing == null)
                return Task.FromResult(false);

            existing.SetDescripcion(materia.Descripcion);
            existing.SetIdPlan(materia.IdPlan);
            existing.SetHoras(materia.HsSemanales, materia.HsTotales);

            return Task.FromResult(true);
        }

        public Task<IEnumerable<Materia>> GetByCriteriaAsync(MateriaCriteria criteria)
        {
            string searchTerm = criteria.Texto.ToLower();

            IEnumerable<Materia> result = materias.Where(m =>
                m.Descripcion.ToLower().Contains(searchTerm)
            ).OrderBy(m => m.Descripcion).ToList();

            return Task.FromResult(result);
        }
    }
}
