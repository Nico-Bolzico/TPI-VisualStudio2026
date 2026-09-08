using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface ITokenService
    {
        string GenerarToken(UsuarioDTO usuario);
    }
}
