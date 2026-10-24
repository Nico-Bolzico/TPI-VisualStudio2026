using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly TPIContext context;

        public UsuarioRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task<Usuario?> GetByNombreUsuarioAsync(string nombreUsuario)
        {
            return await context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> NombreUsuarioExistsAsync(string nombreUsuario)
        {
            return await context.Usuarios.AnyAsync(u => u.NombreUsuario == nombreUsuario);
        }

        public async Task AddAsync(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            context.Usuarios.Update(usuario);
            await context.SaveChangesAsync();
        }

        public async Task RegistrarConPersonaAsync(Persona persona, string nombreUsuario, string passwordHash,
            IEnumerable<PermisoUsuarioInfo> permisosIniciales)
        {
            await using var transaccion = await context.Database.BeginTransactionAsync();

            // 1) Persona (la base le asigna el Id)
            context.Personas.Add(persona);
            await context.SaveChangesAsync();

            // 2) Usuario asociado (necesita el Id de la persona)
            var usuario = new Usuario(0, nombreUsuario, passwordHash,
                habilitado: true, cambiaClave: false, idPersona: persona.Id);
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            // 3) Permisos por defecto: una fila por modulo existente
            var modulos = await context.Modulos.ToListAsync();
            foreach (var permiso in permisosIniciales)
            {
                var modulo = modulos.FirstOrDefault(m => m.Descripcion == permiso.Modulo);
                if (modulo == null)
                    continue;

                context.ModulosUsuarios.Add(new ModuloUsuario(0, modulo.Id, usuario.Id,
                    permiso.PuedeAlta, permiso.PuedeBaja, permiso.PuedeModificar, permiso.PuedeConsultar));
            }
            await context.SaveChangesAsync();

            await transaccion.CommitAsync();
        }
    }
}
