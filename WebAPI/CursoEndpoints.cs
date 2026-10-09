using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class CursoEndpoints
    {
        public static void MapCursoEndpoints(this WebApplication app)
        {
            app.MapGet("/cursos/materia/{idMateria:int}", async (int idMateria, ICursoService cursoService) =>
            {
                var result = await cursoService.GetByMateriaAsync(idMateria);
                return Results.Ok(result);
            })
            .WithName("GetCursosByMateria")
            .Produces<List<CursoDTO>>(StatusCodes.Status200OK)
            .RequireAuthorization("Cursos.Consultar")
            .WithOpenApi();

            app.MapGet("/cursos/comision/{idComision:int}", async (int idComision, ICursoService cursoService) =>
            {
                var result = await cursoService.GetByComisionAsync(idComision);
                return Results.Ok(result);
            })
            .WithName("GetCursosByComision")
            .Produces<List<CursoDTO>>(StatusCodes.Status200OK)
            .RequireAuthorization("Cursos.Consultar")
            .WithOpenApi();

            app.MapGet("/cursos/{id:int}", async (int id, ICursoService cursoService) =>
            {
                var dto = await cursoService.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetCurso")
            .Produces<CursoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization("Cursos.Consultar")
            .WithOpenApi();

            app.MapGet("/cursos", async (ICursoService cursoService) =>
            {
                var dtos = await cursoService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllCursos")
            .Produces<List<CursoDTO>>(StatusCodes.Status200OK)
            .RequireAuthorization("Cursos.Consultar")
            .WithOpenApi();

            app.MapPost("/cursos", async (CursoDTO dto, ICursoService cursoService) =>
            {
                try
                {
                    var created = await cursoService.AddAsync(dto);
                    return Results.Created($"/cursos/{created.Id}", created);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddCurso")
            .Produces<CursoDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization("Cursos.Alta")
            .WithOpenApi();

            app.MapPut("/cursos", async (CursoDTO dto, ICursoService cursoService) =>
            {
                try
                {
                    var found = await cursoService.UpdateAsync(dto);

                    if (!found)
                    {
                        return Results.NotFound();
                    }

                    return Results.NoContent();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("UpdateCurso")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization("Cursos.Modificar")
            .WithOpenApi();

            app.MapDelete("/cursos/{id:int}", async (int id, ICursoService cursoService) =>
            {
                var deleted = await cursoService.DeleteAsync(id);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeleteCurso")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization("Cursos.Baja")
            .WithOpenApi();
        }
    }
}
