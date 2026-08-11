namespace DTOs
{
    public class PersonaDTO
    {
        public int Id { get; set; }
        public int Legajo { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }

        // "Alumno" o "Profesor"
        public string TipoPersona { get; set; } = string.Empty;

        // Requerido si TipoPersona = Alumno
        public int? IdPlan { get; set; }
    }
}
