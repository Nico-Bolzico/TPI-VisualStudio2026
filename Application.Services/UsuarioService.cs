using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain.Model;
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

        public async Task<PerfilDTO?> GetPerfilAsync(int idUsuario)
        {
            var (usuario, persona) = await ObtenerCuentaAsync(idUsuario);
            if (usuario == null || persona == null)
                return null;

            return new PerfilDTO
            {
                NombreUsuario = usuario.NombreUsuario,
                Legajo = persona.Legajo,
                TipoPersona = persona.TipoPersona.ToString(),
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                Direccion = persona.Direccion,
                Email = persona.Email,
                Telefono = persona.Telefono,
                FechaNacimiento = persona.FechaNacimiento
            };
        }

        // Solo se pueden cambiar los datos personales. Legajo, TipoPersona y Plan se conservan
        // tal cual están: los define la facultad, no la propia persona.
        public async Task<bool> UpdatePerfilAsync(int idUsuario, PerfilDTO dto)
        {
            var (usuario, existente) = await ObtenerCuentaAsync(idUsuario);
            if (usuario == null || existente == null)
                return false;

            var actualizada = new Persona(existente.Id, existente.Legajo,
                (dto.Nombre ?? string.Empty).Trim(),
                (dto.Apellido ?? string.Empty).Trim(),
                (dto.Direccion ?? string.Empty).Trim(),
                (dto.Email ?? string.Empty).Trim(),
                (dto.Telefono ?? string.Empty).Trim(),
                dto.FechaNacimiento,
                existente.TipoPersona,
                existente.IdPlan);

            return await personaRepository.UpdateAsync(actualizada);
        }

        public async Task<bool> CambiarPasswordAsync(int idUsuario, CambioPasswordDTO dto)
        {
            var usuario = await usuarioRepository.GetByIdAsync(idUsuario);
            if (usuario == null || !usuario.Habilitado)
                return false;

            if (string.IsNullOrEmpty(dto.PasswordActual)
                || !PasswordHasher.Verify(dto.PasswordActual, usuario.PasswordHash))
            {
                throw new ArgumentException("La contraseña actual no es correcta.");
            }

            PasswordPolicy.Validar(dto.PasswordNueva);

            if (dto.PasswordNueva == dto.PasswordActual)
                throw new ArgumentException("La contraseña nueva debe ser distinta de la actual.");

            usuario.SetPasswordHash(PasswordHasher.Hash(dto.PasswordNueva));
            usuario.SetCambiaClave(false);
            await usuarioRepository.UpdateAsync(usuario);
            return true;
        }

        private async Task<(Usuario? usuario, Persona? persona)> ObtenerCuentaAsync(int idUsuario)
        {
            var usuario = await usuarioRepository.GetByIdAsync(idUsuario);
            if (usuario == null || !usuario.Habilitado)
                return (null, null);

            var persona = await personaRepository.GetAsync(usuario.IdPersona);
            return (usuario, persona);
        }
    }
}
