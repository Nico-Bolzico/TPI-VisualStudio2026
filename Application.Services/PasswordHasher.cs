using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Application.Services
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16;   // 128 bits
        private const int KeySize = 32;    // 256 bits
        private const int Iterations = 100_000;

        // Genera un hash nuevo (con salt aleatorio) a partir de una contraseña en texto plano.
        public static string Hash(string password)
        {
            using var algorithm = new Rfc2898DeriveBytes(
                password, SaltSize, Iterations, HashAlgorithmName.SHA256);

            byte[] salt = algorithm.Salt;
            byte[] key = algorithm.GetBytes(KeySize);

            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
        }

        // Verifica si una contraseña en texto plano coincide con un hash ya almacenado.
        public static bool Verify(string password, string hashedPassword)
        {
            var parts = hashedPassword.Split('.');
            if (parts.Length != 2)
                return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] key = Convert.FromBase64String(parts[1]);

            using var algorithm = new Rfc2898DeriveBytes(
                password, salt, Iterations, HashAlgorithmName.SHA256);

            byte[] keyToCheck = algorithm.GetBytes(KeySize);

            // FixedTimeEquals evita "timing attacks"
            return CryptographicOperations.FixedTimeEquals(keyToCheck, key);
        }
    }
}