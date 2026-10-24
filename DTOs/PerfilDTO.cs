namespace DTOs
{
    // Perfil del usuario autenticado (GET/PUT /perfil).
    // Editables: Nombre, Apellido, Direccion, Email, Telefono y FechaNacimiento.
    // De solo lectura (el servidor los ignora en el PUT): NombreUsuario, Legajo y TipoPersona.
    public class PerfilDTO
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public int Legajo { get; set; }
        public string TipoPersona { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
    }
}
