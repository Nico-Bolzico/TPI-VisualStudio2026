using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class ComisionService : IComisionService
    {
        private readonly IComisionRepository comisionRepository;

        public ComisionService(IComisionRepository comisionRepository)
        {
            this.comisionRepository = comisionRepository;
        }

        public async Task<ComisionDTO> AddAsync(ComisionDTO dto)
        {
            var comision = new Comision(0, dto.Descripcion, dto.AnioEspecialidad, dto.IdPlan);
            await comisionRepository.AddAsync(comision);

            return ToDto(comision);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await comisionRepository.DeleteAsync(id);
        }

        public async Task<ComisionDTO?> GetAsync(int id)
        {
            var comision = await comisionRepository.GetAsync(id);
            return comision == null ? null : ToDto(comision);
        }

        public async Task<IEnumerable<ComisionDTO>> GetAllAsync()
        {
            var list = await comisionRepository.GetAllAsync();
            return list.Select(ToDto).ToList();
        }

        public async Task<IEnumerable<ComisionDTO>> GetByCriteriaAsync(ComisionCriteriaDTO criteriaDTO)
        {
            var criteria = new ComisionCriteria(criteriaDTO.Texto);
            var list = await comisionRepository.GetByCriteriaAsync(criteria);
            return list.Select(ToDto).ToList();
        }

        public async Task<bool> UpdateAsync(ComisionDTO dto)
        {
            var comision = new Comision(dto.Id, dto.Descripcion, dto.AnioEspecialidad, dto.IdPlan);
            return await comisionRepository.UpdateAsync(comision);
        }

        private static ComisionDTO ToDto(Comision comision)
        {
            return new ComisionDTO
            {
                Id = comision.Id,
                Descripcion = comision.Descripcion,
                AnioEspecialidad = comision.AnioEspecialidad,
                IdPlan = comision.IdPlan
            };
        }
    }
}
