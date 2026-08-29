using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string NombreUsuario { get; private set; }
        public string PasswordHash { get; private set; }
        public bool Habilitado { get; private set; }
        public bool CambiaClave { get; private set; }
        public int IdPersona { get; private set; }

        public Usuario(int id, string nombreUsuario, string passwordHash, bool habilitado,
            bool cambiaClave, int idPersona)
        {
            SetId(id);
            SetNombreUsuario(nombreUsuario);
            SetPasswordHash(passwordHash);
            Habilitado = habilitado;
            CambiaClave = cambiaClave;
            SetIdPersona(idPersona);
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor o igual que 0.", nameof(id));
            Id = id;
        }

        public void SetNombreUsuario(string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new ArgumentException("El nombre de usuario no puede ser nulo o vacío.", nameof(nombreUsuario));
            NombreUsuario = nombreUsuario;
        }

        public void SetPasswordHash(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("El hash de contraseña no puede ser nulo o vacío.", nameof(passwordHash));
            PasswordHash = passwordHash;
        }

        public void SetHabilitado(bool habilitado)
        {
            Habilitado = habilitado;
        }

        public void SetCambiaClave(bool cambiaClave)
        {
            CambiaClave = cambiaClave;
        }

        public void SetIdPersona(int idPersona)
        {
            if (idPersona <= 0)
                throw new ArgumentException("El IdPersona debe ser mayor que 0.", nameof(idPersona));
            IdPersona = idPersona;
        }
    }
}
