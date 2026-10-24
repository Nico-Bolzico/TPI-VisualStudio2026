using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface IUsuarioService
    {
        Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO dto);

        // Perfil propio. Devuelven null / false si el usuario no existe o está deshabilitado.
        Task<PerfilDTO?> GetPerfilAsync(int idUsuario);
        Task<bool> UpdatePerfilAsync(int idUsuario, PerfilDTO dto);

        // Devuelve false si el usuario no existe o está deshabilitado.
        // Lanza ArgumentException si la contraseña actual no coincide o la nueva no cumple las reglas.
        Task<bool> CambiarPasswordAsync(int idUsuario, CambioPasswordDTO dto);
    }
}
