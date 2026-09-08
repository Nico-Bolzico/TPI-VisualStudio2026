using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public UsuarioDTO Usuario { get; set; } = new();
        public List<PermisoDTO> Permisos { get; set; } = new();
    }
}
