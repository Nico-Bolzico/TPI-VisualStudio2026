using System.Security.Claims;
using Application.Services;
using DTOs;

namespace WebAPI
{
    // Perfil propio: cualquier usuario autenticado puede ver y modificar SUS datos personales.
    public static class PerfilEndpoints
    {
        public static void MapPerfilEndpoints(this WebApplication app)
        {
            app.MapGet("/perfil", async (ClaimsPrincipal user, IUsuarioService usuarioService) =>
            {
                if (!TryObtenerIdUsuario(user, out int idUsuario))
                    return Results.Unauthorized();

                var perfil = await usuarioService.GetPerfilAsync(idUsuario);

                return perfil == null ? Results.Unauthorized() : Results.Ok(perfil);
            })
            .WithName("GetPerfil")
            .Produces<PerfilDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .RequireAuthorization()
            .WithOpenApi();

            app.MapPut("/perfil", async (PerfilDTO dto, ClaimsPrincipal user, IUsuarioService usuarioService) =>
            {
                if (!TryObtenerIdUsuario(user, out int idUsuario))
                    return Results.Unauthorized();

                try
                {
                    bool actualizado = await usuarioService.UpdatePerfilAsync(idUsuario, dto);

                    return actualizado ? Results.NoContent() : Results.Unauthorized();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("UpdatePerfil")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .RequireAuthorization()
            .WithOpenApi();

            // Un 400 (no un 401) cuando la contraseña actual es incorrecta: los clientes interpretan
            // el 401 como "sesión vencida".
            app.MapPut("/perfil/password", async (CambioPasswordDTO dto, ClaimsPrincipal user, IUsuarioService usuarioService) =>
            {
                if (!TryObtenerIdUsuario(user, out int idUsuario))
                    return Results.Unauthorized();

                try
                {
                    bool cambiada = await usuarioService.CambiarPasswordAsync(idUsuario, dto);

                    return cambiada ? Results.NoContent() : Results.Unauthorized();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("CambiarPassword")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .RequireAuthorization()
            .WithOpenApi();
        }

        private static bool TryObtenerIdUsuario(ClaimsPrincipal user, out int idUsuario)
        {
            var claim = user.FindFirst(ClaimTypes.NameIdentifier);
            idUsuario = 0;
            return claim != null && int.TryParse(claim.Value, out idUsuario);
        }
    }
}
