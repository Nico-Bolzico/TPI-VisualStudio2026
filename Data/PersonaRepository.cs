using Domain.Model;

namespace Data
{
    public class PersonaRepository : IPersonaRepository
    {
        private static readonly List<Persona> personas = new List<Persona>();
        private static int nextId = 1;

        public Task AddAsync(Persona persona)
        {
            // Simular auto-increment de ID
            persona.SetId(nextId);
            nextId++;

            personas.Add(persona);
            return Task.CompletedTask;
        }

        public Task<bool> DeleteAsync(int id)
        {
            var persona = personas.FirstOrDefault(p => p.Id == id);
            if (persona != null)
            {
                personas.Remove(persona);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<Persona?> GetAsync(int id)
        {
            return Task.FromResult(personas.FirstOrDefault(p => p.Id == id));
        }

        public Task<IEnumerable<Persona>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Persona>>(personas.ToList());
        }

        public Task<bool> UpdateAsync(Persona persona)
        {
            var existing = personas.FirstOrDefault(p => p.Id == persona.Id);
            if (existing == null)
                return Task.FromResult(false);

            existing.SetLegajo(persona.Legajo);
            existing.SetNombre(persona.Nombre);
            existing.SetApellido(persona.Apellido);
            existing.SetDireccion(persona.Direccion);
            existing.SetEmail(persona.Email);
            existing.SetTelefono(persona.Telefono);
            existing.SetFechaNacimiento(persona.FechaNacimiento);
            existing.SetTipoPersona(persona.TipoPersona);
            existing.SetIdPlan(persona.IdPlan);

            return Task.FromResult(true);
        }

        public Task<bool> LegajoExistsAsync(int legajo, int? excludeId = null)
        {
            var query = personas.Where(p => p.Legajo == legajo);
            if (excludeId.HasValue)
            {
                query = query.Where(p => p.Id != excludeId.Value);
            }
            return Task.FromResult(query.Any());
        }

        public Task<IEnumerable<Persona>> GetByCriteriaAsync(PersonaCriteria criteria)
        {
            string searchTerm = criteria.Texto.ToLower();

            IEnumerable<Persona> result = personas.Where(p =>
                p.Nombre.ToLower().Contains(searchTerm) ||
                p.Apellido.ToLower().Contains(searchTerm) ||
                p.Email.ToLower().Contains(searchTerm) ||
                p.Legajo.ToString().Contains(searchTerm)
            ).OrderBy(p => p.Nombre).ThenBy(p => p.Apellido).ToList();

            return Task.FromResult(result);
        }
    }
}
