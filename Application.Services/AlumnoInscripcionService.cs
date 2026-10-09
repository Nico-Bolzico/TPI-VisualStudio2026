using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class AlumnoInscripcionService : IAlumnoInscripcionService
    {
        private readonly IAlumnoInscripcionRepository alumnoInscripcionRepository;
        private readonly ICursoRepository cursoRepository;

        public AlumnoInscripcionService(
            IAlumnoInscripcionRepository alumnoInscripcionRepository,
            ICursoRepository cursoRepository)
        {
            this.alumnoInscripcionRepository = alumnoInscripcionRepository;
            this.cursoRepository = cursoRepository;
        }

        public async Task<AlumnoInscripcionDTO> AddAsync(AlumnoInscripcionDTO dto)
        {
            var curso = await cursoRepository.GetAsync(dto.IdCurso);
            if (curso == null)
            {
                throw new ArgumentException("El curso especificado no existe.");
            }

            bool yaInscripto = await alumnoInscripcionRepository.YaEstaInscriptoAsync(dto.IdAlumno, dto.IdCurso);
            if (yaInscripto)
            {
                throw new InvalidOperationException("El alumno ya se encuentra inscripto en este curso.");
            }

            int inscriptosActuales = await alumnoInscripcionRepository.ContarInscriptosAsync(dto.IdCurso);
            if (inscriptosActuales >= curso.Cupo)
            {
                throw new InvalidOperationException($"No hay cupo disponible para este curso (cupo máximo: {curso.Cupo}).");
            }

            string condicion = string.IsNullOrWhiteSpace(dto.Condicion) ? "Inscripto" : dto.Condicion;
            var inscripcion = new AlumnoInscripcion(0, dto.IdAlumno, dto.IdCurso, condicion, dto.Nota);

            await alumnoInscripcionRepository.AddAsync(inscripcion);

            return ToDto(inscripcion);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await alumnoInscripcionRepository.DeleteAsync(id);
        }

        public async Task<AlumnoInscripcionDTO?> GetAsync(int id)
        {
            var inscripcion = await alumnoInscripcionRepository.GetAsync(id);
            return inscripcion == null ? null : ToDto(inscripcion);
        }

        public async Task<IEnumerable<AlumnoInscripcionDTO>> GetAllAsync()
        {
            var list = await alumnoInscripcionRepository.GetAllAsync();
            return list.Select(ToDto).ToList();
        }

        public async Task<IEnumerable<AlumnoInscripcionDTO>> GetByCursoAsync(int idCurso)
        {
            var list = await alumnoInscripcionRepository.GetByCursoAsync(idCurso);
            return list.Select(ToDto).ToList();
        }

        public async Task<IEnumerable<AlumnoInscripcionDTO>> GetByAlumnoAsync(int idAlumno)
        {
            var list = await alumnoInscripcionRepository.GetByAlumnoAsync(idAlumno);
            return list.Select(ToDto).ToList();
        }

        public async Task<bool> UpdateAsync(AlumnoInscripcionDTO dto)
        {
            var inscripcion = new AlumnoInscripcion(dto.Id, dto.IdAlumno, dto.IdCurso, dto.Condicion, dto.Nota);
            return await alumnoInscripcionRepository.UpdateAsync(inscripcion);
        }

        private static AlumnoInscripcionDTO ToDto(AlumnoInscripcion inscripcion)
        {
            return new AlumnoInscripcionDTO
            {
                Id = inscripcion.Id,
                IdAlumno = inscripcion.IdAlumno,
                IdCurso = inscripcion.IdCurso,
                Condicion = inscripcion.Condicion,
                Nota = inscripcion.Nota
            };
        }
    }
}
