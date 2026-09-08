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
        private readonly IModuloUsuarioRepository moduloUsuarioRepository;
        private readonly ITokenService tokenService;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IPersonaRepository personaRepository,
            IModuloUsuarioRepository moduloUsuarioRepository,
            ITokenService tokenService)
        {
            this.usuarioRepository = usuarioRepository;
            this.personaRepository = personaRepository;
            this.moduloUsuarioRepository = moduloUsuarioRepository;
            this.tokenService = tokenService;
        }

        public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO dto)
        {
            var usuario = await usuarioRepository.GetByNombreUsuarioAsync(dto.NombreUsuario);

            if (usuario == null)
                return null;

            if (!usuario.Habilitado)
                return null;

            if (!PasswordHasher.Verify(dto.Password, usuario.PasswordHash))
                return null;

            var persona = await personaRepository.GetAsync(usuario.IdPersona);

            var usuarioDto = new UsuarioDTO
            {
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                CambiaClave = usuario.CambiaClave,
                IdPersona = usuario.IdPersona,
                Nombre = persona?.Nombre ?? string.Empty,
                Apellido = persona?.Apellido ?? string.Empty,
                Email = persona?.Email ?? string.Empty
            };

            var permisos = await moduloUsuarioRepository.GetPermisosByUsuarioAsync(usuario.Id);

            return new LoginResponseDTO
            {
                Token = tokenService.GenerarToken(usuarioDto),
                Usuario = usuarioDto,
                Permisos = permisos.Select(p => new PermisoDTO
                {
                    Modulo = p.Modulo,
                    PuedeAlta = p.PuedeAlta,
                    PuedeBaja = p.PuedeBaja,
                    PuedeModificar = p.PuedeModificar,
                    PuedeConsultar = p.PuedeConsultar
                }).ToList()
            };
        }
    }
}
