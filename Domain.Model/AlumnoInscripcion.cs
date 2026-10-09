namespace Domain.Model
{
    public class AlumnoInscripcion
    {
        public int Id { get; private set; }
        public int IdAlumno { get; private set; }
        public int IdCurso { get; private set; }
        public string Condicion { get; private set; }
        public int? Nota { get; private set; }

        public AlumnoInscripcion(int id, int idAlumno, int idCurso, string condicion = "Inscripto", int? nota = null)
        {
            SetId(id);
            SetIdAlumno(idAlumno);
            SetIdCurso(idCurso);
            SetCondicion(condicion);
            SetNota(nota);
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor o igual que 0.", nameof(id));
            Id = id;
        }

        public void SetIdAlumno(int idAlumno)
        {
            if (idAlumno <= 0)
                throw new ArgumentException("El IdAlumno debe ser mayor que 0.", nameof(idAlumno));
            IdAlumno = idAlumno;
        }

        public void SetIdCurso(int idCurso)
        {
            if (idCurso <= 0)
                throw new ArgumentException("El IdCurso debe ser mayor que 0.", nameof(idCurso));
            IdCurso = idCurso;
        }

        public void SetCondicion(string condicion)
        {
            if (string.IsNullOrWhiteSpace(condicion))
                throw new ArgumentException("La condición no puede ser nula o vacía.", nameof(condicion));
            Condicion = condicion;
        }

        public void SetNota(int? nota)
        {
            if (nota.HasValue && (nota.Value < 1 || nota.Value > 10))
                throw new ArgumentException("La nota debe estar entre 1 y 10.", nameof(nota));
            Nota = nota;
        }
    }
}
