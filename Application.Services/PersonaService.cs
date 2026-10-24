using Domain.Model;
using Data;
using DTOs;

namespace Application.Services
{
    public class PersonaService : IPersonaService
    {
        private const int NombreUsuarioMinimo = 4;
        private const int NombreUsuarioMaximo = 50;

        private readonly IPersonaRepository personaRepository;
        private readonly IUsuarioRepository usuarioRepository;

        public PersonaService(IPersonaRepository personaRepository, IUsuarioRepository usuarioRepository)
        {
            this.personaRepository = personaRepository;
            this.usuarioRepository = usuarioRepository;
        }

        // Da de alta la persona y, en la misma operación, su cuenta de acceso
        // con los permisos por defecto correspondientes a su tipo (ver PermisosPorDefecto).
        public async Task<PersonaDTO> AddAsync(PersonaAltaDTO dto)
        {
            // Validar que el Legajo no esté duplicado
            if (await personaRepository.LegajoExistsAsync(dto.Legajo))
            {
                throw new ArgumentException($"Ya existe una Persona con el Legajo '{dto.Legajo}'.");
            }

            string nombreUsuario = (dto.NombreUsuario ?? string.Empty).Trim();
            ValidarCredenciales(nombreUsuario, dto.Password);

            if (await usuarioRepository.NombreUsuarioExistsAsync(nombreUsuario))
            {
                throw new ArgumentException($"El nombre de usuario '{nombreUsuario}' ya está en uso.");
            }

            var tipoPersona = ParseTipoPersona(dto.TipoPersona);
            Persona persona = new Persona(0, dto.Legajo, dto.Nombre, dto.Apellido, dto.Direccion,
                dto.Email, dto.Telefono, dto.FechaNacimiento, tipoPersona, dto.IdPlan);

            await usuarioRepository.RegistrarConPersonaAsync(
                persona,
                nombreUsuario,
                PasswordHasher.Hash(dto.Password),
                PermisosPorDefecto.Para(tipoPersona));

            // Se devuelve la persona sin datos de la cuenta (nunca se devuelve la contraseña)
            return ToDto(persona);
        }

        private static void ValidarCredenciales(string nombreUsuario, string? password)
        {
            if (nombreUsuario.Length < NombreUsuarioMinimo || nombreUsuario.Length > NombreUsuarioMaximo)
            {
                throw new ArgumentException(
                    $"El nombre de usuario debe tener entre {NombreUsuarioMinimo} y {NombreUsuarioMaximo} caracteres.");
            }

            if (!nombreUsuario.All(c => char.IsLetterOrDigit(c) || c is '.' or '_' or '-'))
            {
                throw new ArgumentException(
                    "El nombre de usuario solo puede contener letras, números, punto, guion y guion bajo.");
            }

            PasswordPolicy.Validar(password);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await personaRepository.DeleteAsync(id);
        }

        public async Task<PersonaDTO?> GetAsync(int id)
        {
            Persona? persona = await personaRepository.GetAsync(id);

            if (persona == null)
                return null;

            return ToDto(persona);
        }

        public async Task<IEnumerable<PersonaDTO>> GetAllAsync()
        {
            var personas = await personaRepository.GetAllAsync();
            return personas.Select(ToDto).ToList();
        }

        public async Task<bool> UpdateAsync(PersonaDTO dto)
        {
            // Validar que el Legajo no esté duplicado (excluyendo la persona actual)
            if (await personaRepository.LegajoExistsAsync(dto.Legajo, dto.Id))
            {
                throw new ArgumentException($"Ya existe otra Persona con el Legajo '{dto.Legajo}'.");
            }

            var existing = await personaRepository.GetAsync(dto.Id);
            if (existing == null)
                return false;

            var tipoPersona = ParseTipoPersona(dto.TipoPersona);
            Persona persona = new Persona(dto.Id, dto.Legajo, dto.Nombre, dto.Apellido, dto.Direccion,
                dto.Email, dto.Telefono, dto.FechaNacimiento, tipoPersona, dto.IdPlan);

            return await personaRepository.UpdateAsync(persona);
        }

        public async Task<IEnumerable<PersonaDTO>> GetByCriteriaAsync(PersonaCriteriaDTO criteriaDTO)
        {
            var criteria = new PersonaCriteria(criteriaDTO.Texto);
            var personas = await personaRepository.GetByCriteriaAsync(criteria);
            return personas.Select(ToDto);
        }

        private static TipoPersona ParseTipoPersona(string tipoPersona)
        {
            if (!Enum.TryParse<TipoPersona>(tipoPersona, ignoreCase: true, out var result))
            {
                throw new ArgumentException(
                    $"TipoPersona '{tipoPersona}' no es válido. Valores permitidos: Alumno, Profesor.");
            }
            return result;
        }

        private static PersonaDTO ToDto(Persona persona)
        {
            return new PersonaDTO
            {
                Id = persona.Id,
                Legajo = persona.Legajo,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                Direccion = persona.Direccion,
                Email = persona.Email,
                Telefono = persona.Telefono,
                FechaNacimiento = persona.FechaNacimiento,
                TipoPersona = persona.TipoPersona.ToString(),
                IdPlan = persona.IdPlan
            };
        }
    }
}
