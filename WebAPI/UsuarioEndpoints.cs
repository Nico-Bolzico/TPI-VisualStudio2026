using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class UsuarioEndpoints
    {
        public static void MapUsuarioEndpoints(this WebApplication app)
        {
            app.MapPost("/login", async (LoginRequestDTO dto, IUsuarioService usuarioService) =>
            {
                var usuario = await usuarioService.LoginAsync(dto);

                if (usuario == null)
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(usuario);
            })
            .WithName("Login")
            .Produces<UsuarioDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithOpenApi();
        }
    }
}
