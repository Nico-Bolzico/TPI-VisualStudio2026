using Domain.Model;
using Data;
using DTOs;

namespace Application.Services
{
    public class MateriaService : IMateriaService
    {
        private readonly IMateriaRepository materiaRepository;

        public MateriaService(IMateriaRepository materiaRepository)
        {
            this.materiaRepository = materiaRepository;
        }

        public async Task<MateriaDTO> AddAsync(MateriaDTO dto)
        {
            Materia materia = new Materia(0, dto.Descripcion, dto.HsSemanales, dto.HsTotales, dto.IdPlan);

            await materiaRepository.AddAsync(materia);

            dto.Id = materia.Id;
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await materiaRepository.DeleteAsync(id);
        }

        public async Task<MateriaDTO?> GetAsync(int id)
        {
            Materia? materia = await materiaRepository.GetAsync(id);

            if (materia == null)
                return null;

            return ToDto(materia);
        }

        public async Task<IEnumerable<MateriaDTO>> GetAllAsync()
        {
            var materias = await materiaRepository.GetAllAsync();
            return materias.Select(ToDto).ToList();
        }

        public async Task<bool> UpdateAsync(MateriaDTO dto)
        {
            var existing = await materiaRepository.GetAsync(dto.Id);
            if (existing == null)
                return false;

            Materia materia = new Materia(dto.Id, dto.Descripcion, dto.HsSemanales, dto.HsTotales, dto.IdPlan);
            return await materiaRepository.UpdateAsync(materia);
        }

        public async Task<IEnumerable<MateriaDTO>> GetByCriteriaAsync(MateriaCriteriaDTO criteriaDTO)
        {
            var criteria = new MateriaCriteria(criteriaDTO.Texto);
            var materias = await materiaRepository.GetByCriteriaAsync(criteria);
            return materias.Select(ToDto);
        }

        private static MateriaDTO ToDto(Materia materia)
        {
            return new MateriaDTO
            {
                Id = materia.Id,
                Descripcion = materia.Descripcion,
                HsSemanales = materia.HsSemanales,
                HsTotales = materia.HsTotales,
                IdPlan = materia.IdPlan
            };
        }
    }
}
