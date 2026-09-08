using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Data/ModuloUsuarioRepository.cs
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ModuloUsuarioRepository : IModuloUsuarioRepository
    {
        private readonly TPIContext context;

        public ModuloUsuarioRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<PermisoUsuarioInfo>> GetPermisosByUsuarioAsync(int idUsuario)
        {
            var query =
                from mu in context.ModulosUsuarios
                join m in context.Modulos on mu.IdModulo equals m.Id
                where mu.IdUsuario == idUsuario && m.Activo
                select new PermisoUsuarioInfo(
                    m.Descripcion, mu.PuedeAlta, mu.PuedeBaja, mu.PuedeModificar, mu.PuedeConsultar);

            return await query.ToListAsync();
        }

        public async Task<bool> TienePermisoAsync(int idUsuario, string modulo, string accion)
        {
            var permisos = await GetPermisosByUsuarioAsync(idUsuario);
            var permiso = permisos.FirstOrDefault(p => p.Modulo == modulo);

            if (permiso == null)
                return false;

            return accion switch
            {
                "Alta" => permiso.PuedeAlta,
                "Baja" => permiso.PuedeBaja,
                "Modificar" => permiso.PuedeModificar,
                "Consultar" => permiso.PuedeConsultar,
                _ => false
            };
        }
    }
}
