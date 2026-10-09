using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class EspecialidadService : IEspecialidadService
    {
        private readonly IEspecialidadRepository especialidadRepository;

        public EspecialidadService(IEspecialidadRepository especialidadRepository)
        {
            this.especialidadRepository = especialidadRepository;
        }

        public async Task<EspecialidadDTO> AddAsync(EspecialidadDTO dto)
        {
            var especialidad = new Especialidad(0, dto.Descripcion);
            await especialidadRepository.AddAsync(especialidad);

            return ToDto(especialidad);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await especialidadRepository.DeleteAsync(id);
        }

        public async Task<EspecialidadDTO?> GetAsync(int id)
        {
            var especialidad = await especialidadRepository.GetAsync(id);
            return especialidad == null ? null : ToDto(especialidad);
        }

        public async Task<IEnumerable<EspecialidadDTO>> GetAllAsync()
        {
            var list = await especialidadRepository.GetAllAsync();
            return list.Select(ToDto).ToList();
        }

        public async Task<IEnumerable<EspecialidadDTO>> GetByCriteriaAsync(EspecialidadCriteriaDTO criteriaDTO)
        {
            var criteria = new EspecialidadCriteria(criteriaDTO.Texto);
            var list = await especialidadRepository.GetByCriteriaAsync(criteria);
            return list.Select(ToDto).ToList();
        }

        public async Task<bool> UpdateAsync(EspecialidadDTO dto)
        {
            var especialidad = new Especialidad(dto.Id, dto.Descripcion);
            return await especialidadRepository.UpdateAsync(especialidad);
        }

        private static EspecialidadDTO ToDto(Especialidad especialidad)
        {
            return new EspecialidadDTO
            {
                Id = especialidad.Id,
                Descripcion = especialidad.Descripcion
            };
        }
    }
}
