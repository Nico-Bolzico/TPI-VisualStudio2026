using DTOs;

namespace Application.Services
{
    public interface IAlumnoInscripcionService
    {
        Task<AlumnoInscripcionDTO> AddAsync(AlumnoInscripcionDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<AlumnoInscripcionDTO?> GetAsync(int id);
        Task<IEnumerable<AlumnoInscripcionDTO>> GetAllAsync();
        Task<IEnumerable<AlumnoInscripcionDTO>> GetByCursoAsync(int idCurso);
        Task<IEnumerable<AlumnoInscripcionDTO>> GetByAlumnoAsync(int idAlumno);
        Task<bool> UpdateAsync(AlumnoInscripcionDTO dto);
    }
}
