namespace DTOs
{
    // Alta de una persona junto con su cuenta de acceso.
    // Solo se usa en POST /personas; la contraseña nunca se devuelve en ninguna respuesta.
    public class PersonaAltaDTO : PersonaDTO
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
