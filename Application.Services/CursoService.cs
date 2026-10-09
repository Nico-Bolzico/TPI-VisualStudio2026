using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository cursoRepository;

        public CursoService(ICursoRepository cursoRepository)
        {
            this.cursoRepository = cursoRepository;
        }

        public async Task<CursoDTO> AddAsync(CursoDTO dto)
        {
            var curso = new Curso(0, dto.IdMateria, dto.IdComision, dto.AnioCalendario, dto.Cupo);
            await cursoRepository.AddAsync(curso);

            return ToDto(curso);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await cursoRepository.DeleteAsync(id);
        }

        public async Task<CursoDTO?> GetAsync(int id)
        {
            var curso = await cursoRepository.GetAsync(id);
            return curso == null ? null : ToDto(curso);
        }

        public async Task<IEnumerable<CursoDTO>> GetAllAsync()
        {
            var list = await cursoRepository.GetAllAsync();
            return list.Select(ToDto).ToList();
        }

        public async Task<IEnumerable<CursoDTO>> GetByMateriaAsync(int idMateria)
        {
            var list = await cursoRepository.GetByMateriaAsync(idMateria);
            return list.Select(ToDto).ToList();
        }

        public async Task<IEnumerable<CursoDTO>> GetByComisionAsync(int idComision)
        {
            var list = await cursoRepository.GetByComisionAsync(idComision);
            return list.Select(ToDto).ToList();
        }

        public async Task<bool> UpdateAsync(CursoDTO dto)
        {
            var curso = new Curso(dto.Id, dto.IdMateria, dto.IdComision, dto.AnioCalendario, dto.Cupo);
            return await cursoRepository.UpdateAsync(curso);
        }

        private static CursoDTO ToDto(Curso curso)
        {
            return new CursoDTO
            {
                Id = curso.Id,
                IdMateria = curso.IdMateria,
                IdComision = curso.IdComision,
                AnioCalendario = curso.AnioCalendario,
                Cupo = curso.Cupo
            };
        }
    }
}
