namespace DTOs
{
    // Cambio de contraseña propio (PUT /perfil/password). Se exige la contraseña actual como confirmación.
    public class CambioPasswordDTO
    {
        public string PasswordActual { get; set; } = string.Empty;
        public string PasswordNueva { get; set; } = string.Empty;
    }
}
