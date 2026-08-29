using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using DTOs;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IPersonaRepository personaRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, IPersonaRepository personaRepository)
        {
            this.usuarioRepository = usuarioRepository;
            this.personaRepository = personaRepository;
        }

        public async Task<UsuarioDTO?> LoginAsync(LoginRequestDTO dto)
        {
            var usuario = await usuarioRepository.GetByNombreUsuarioAsync(dto.NombreUsuario);

            if (usuario == null)
                return null;

            if (!usuario.Habilitado)
                return null;

            if (!PasswordHasher.Verify(dto.Password, usuario.PasswordHash))
                return null;

            var persona = await personaRepository.GetAsync(usuario.IdPersona);

            return new UsuarioDTO
            {
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                CambiaClave = usuario.CambiaClave,
                IdPersona = usuario.IdPersona,
                Nombre = persona?.Nombre ?? string.Empty,
                Apellido = persona?.Apellido ?? string.Empty,
                Email = persona?.Email ?? string.Empty
            };
        }
    }
}
