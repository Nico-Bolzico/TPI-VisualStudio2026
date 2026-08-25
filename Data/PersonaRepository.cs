using Domain.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly TPIContext context;

        public PersonaRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Persona persona)
        {
            context.Personas.Add(persona);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var persona = await context.Personas.FindAsync(id);

            if (persona != null)
            {
                context.Personas.Remove(persona);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Persona?> GetAsync(int id)
        {
            return await context.Personas
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Persona>> GetAllAsync()
        {
            return await context.Personas
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Persona persona)
        {
            var existing = await context.Personas.FindAsync(persona.Id);

            if (existing == null)
                return false;

            existing.SetLegajo(persona.Legajo);
            existing.SetNombre(persona.Nombre);
            existing.SetApellido(persona.Apellido);
            existing.SetDireccion(persona.Direccion);
            existing.SetEmail(persona.Email);
            existing.SetTelefono(persona.Telefono);
            existing.SetFechaNacimiento(persona.FechaNacimiento);
            existing.SetTipoPersona(persona.TipoPersona);
            existing.SetIdPlan(persona.IdPlan);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> LegajoExistsAsync(int legajo, int? excludeId = null)
        {
            var query = context.Personas
                .Where(p => p.Legajo == legajo);

            if (excludeId.HasValue)
            {
                query = query.Where(p => p.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<IEnumerable<Persona>> GetByCriteriaAsync(PersonaCriteria criteria)
        {
            const string sql = @"
                SELECT Id, Legajo, Nombre, Apellido, Direccion, Email, Telefono,FechaNacimiento, TipoPersona, IdPlan
                FROM Personas
                WHERE Nombre LIKE @SearchTerm
                    OR Apellido LIKE @SearchTerm
                    OR Email LIKE @SearchTerm
                    OR CAST(Legajo AS VARCHAR(20)) LIKE @SearchTerm
                ORDER BY Nombre, Apellido";

            var personas = new List<Persona>();

            string connectionString = context.Database.GetConnectionString()
                ?? throw new InvalidOperationException("No se encontró la cadena de conexión.");

            string searchPattern = $"%{criteria.Texto}%";

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@SearchTerm", searchPattern);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                int? idPlan = reader.IsDBNull(9)
                    ? null
                    : reader.GetInt32(9);

                var persona = new Persona(
                    reader.GetInt32(0),                 // Id
                    reader.GetInt32(1),                 // Legajo
                    reader.GetString(2),                // Nombre
                    reader.GetString(3),                // Apellido
                    reader.GetString(4),                // Direccion
                    reader.GetString(5),                // Email
                    reader.GetString(6),                // Telefono
                    reader.GetDateTime(7),              // FechaNacimiento
                    (TipoPersona)reader.GetInt32(8),    // TipoPersona
                    idPlan                              // IdPlan
                );

                personas.Add(persona);
            }

            return personas;
        }
    }
}