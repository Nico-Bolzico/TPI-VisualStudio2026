namespace DTOs
{
    public class AlumnoInscripcionDTO
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public int IdCurso { get; set; }
        public string Condicion { get; set; } = "Inscripto";
        public int? Nota { get; set; }
    }
}
