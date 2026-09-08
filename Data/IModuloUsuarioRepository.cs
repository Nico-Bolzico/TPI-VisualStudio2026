using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

namespace Data
{
    public interface IModuloUsuarioRepository
    {
        Task<IEnumerable<PermisoUsuarioInfo>> GetPermisosByUsuarioAsync(int idUsuario);
        Task<bool> TienePermisoAsync(int idUsuario, string modulo, string accion);
    }
}