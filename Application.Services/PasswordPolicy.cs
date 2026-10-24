namespace Application.Services
{
    // Reglas de contraseña compartidas por el alta de cuentas y el cambio de contraseña.
    public static class PasswordPolicy
    {
        public const int Minimo = 8;
        public const int Maximo = 100;

        public static void Validar(string? password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < Minimo || password.Length > Maximo)
            {
                throw new ArgumentException(
                    $"La contraseña debe tener entre {Minimo} y {Maximo} caracteres.");
            }
        }
    }
}
