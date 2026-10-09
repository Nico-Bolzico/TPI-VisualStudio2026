using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class DocenteCursoService : IDocenteCursoService
    {
        private readonly IDocenteCursoRepository docenteCursoRepository;

        public DocenteCursoService(IDocenteCursoRepository docenteCursoRepository)
        {
            this.docenteCursoRepository = docenteCursoRepository;
        }

        public async Task<DocenteCursoDTO> AddAsync(DocenteCursoDTO dto)
        {
            var docenteCurso = new DocenteCurso(0, dto.IdCurso, dto.IdDocente, dto.Cargo);
            await docenteCursoRepository.AddAsync(docenteCurso);

            return ToDto(docenteCurso);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await docenteCursoRepository.DeleteAsync(id);
        }

        public async Task<DocenteCursoDTO?> GetAsync(int id)
        {
            var entity = await docenteCursoRepository.GetAsync(id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<IEnumerable<DocenteCursoDTO>> GetAllAsync()
        {
            var list = await docenteCursoRepository.GetAllAsync();
            return list.Select(ToDto).ToList();
        }

        public async Task<IEnumerable<DocenteCursoDTO>> GetByCursoAsync(int idCurso)
        {
            var list = await docenteCursoRepository.GetByCursoAsync(idCurso);
            return list.Select(ToDto).ToList();
        }

        public async Task<IEnumerable<DocenteCursoDTO>> GetByDocenteAsync(int idDocente)
        {
            var list = await docenteCursoRepository.GetByDocenteAsync(idDocente);
            return list.Select(ToDto).ToList();
        }

        public async Task<bool> UpdateAsync(DocenteCursoDTO dto)
        {
            var entity = new DocenteCurso(dto.Id, dto.IdCurso, dto.IdDocente, dto.Cargo);
            return await docenteCursoRepository.UpdateAsync(entity);
        }

        private static DocenteCursoDTO ToDto(DocenteCurso entity)
        {
            return new DocenteCursoDTO
            {
                Id = entity.Id,
                IdCurso = entity.IdCurso,
                IdDocente = entity.IdDocente,
                Cargo = entity.Cargo
            };
        }
    }
}
