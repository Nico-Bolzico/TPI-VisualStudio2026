using System.Security.Claims;
using Data;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Authorization
{
    public class PermisoAuthorizationHandler : AuthorizationHandler<PermisoRequirement>
    {
        private readonly IModuloUsuarioRepository moduloUsuarioRepository;

        public PermisoAuthorizationHandler(IModuloUsuarioRepository moduloUsuarioRepository)
        {
            this.moduloUsuarioRepository = moduloUsuarioRepository;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, PermisoRequirement requirement)
        {
            var idClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (idClaim == null || !int.TryParse(idClaim.Value, out int idUsuario))
                return; // sin claim válido -> no se cumple el requisito

            bool tienePermiso = await moduloUsuarioRepository
                .TienePermisoAsync(idUsuario, requirement.Modulo, requirement.Accion);

            if (tienePermiso)
                context.Succeed(requirement);
        }
    }
}