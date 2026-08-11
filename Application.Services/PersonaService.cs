using Domain.Model;
using Data;
using DTOs;

namespace Application.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly IPersonaRepository personaRepository;

        public PersonaService(IPersonaRepository personaRepository)
        {
            this.personaRepository = personaRepository;
        }

        public async Task<PersonaDTO> AddAsync(PersonaDTO dto)
        {
            // Validar que el Legajo no esté duplicado
            if (await personaRepository.LegajoExistsAsync(dto.Legajo))
            {
                throw new ArgumentException($"Ya existe una Persona con el Legajo '{dto.Legajo}'.");
            }

            var tipoPersona = ParseTipoPersona(dto.TipoPersona);
            Persona persona = new Persona(0, dto.Legajo, dto.Nombre, dto.Apellido, dto.Direccion,
                dto.Email, dto.Telefono, dto.FechaNacimiento, tipoPersona, dto.IdPlan);

            await personaRepository.AddAsync(persona);

            dto.Id = persona.Id;
            return dto;
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
