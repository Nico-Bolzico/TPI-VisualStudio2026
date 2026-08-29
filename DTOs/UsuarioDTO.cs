using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public bool CambiaClave { get; set; }
        public int IdPersona { get; set; }

        // Estos datos NO se guardan en Usuario: se completan navegando hacia la Persona asociada (ver UsuarioService.LoginAsync).
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
