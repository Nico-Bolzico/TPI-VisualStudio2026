using Domain.Model;

namespace Data
{
    public interface IAlumnoInscripcionRepository
    {
        Task AddAsync(AlumnoInscripcion inscripcion);
        Task<bool> DeleteAsync(int id);
        Task<AlumnoInscripcion?> GetAsync(int id);
        Task<IEnumerable<AlumnoInscripcion>> GetAllAsync();
        Task<IEnumerable<AlumnoInscripcion>> GetByCursoAsync(int idCurso);
        Task<IEnumerable<AlumnoInscripcion>> GetByAlumnoAsync(int idAlumno);
        Task<bool> UpdateAsync(AlumnoInscripcion inscripcion);
        Task<int> ContarInscriptosAsync(int idCurso);
        Task<bool> YaEstaInscriptoAsync(int idAlumno, int idCurso);
    }
}
