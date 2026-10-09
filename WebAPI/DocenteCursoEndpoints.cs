using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class DocenteCursoEndpoints
    {
        public static void MapDocenteCursoEndpoints(this WebApplication app)
        {
            app.MapGet("/docentes-cursos/curso/{idCurso:int}", async (int idCurso, IDocenteCursoService service) =>
            {
                var result = await service.GetByCursoAsync(idCurso);
                return Results.Ok(result);
            })
            .WithName("GetDocentesCursosByCurso")
            .Produces<List<DocenteCursoDTO>>(StatusCodes.Status200OK)
            .RequireAuthorization("DocentesCursos.Consultar")
            .WithOpenApi();

            app.MapGet("/docentes-cursos/docente/{idDocente:int}", async (int idDocente, IDocenteCursoService service) =>
            {
                var result = await service.GetByDocenteAsync(idDocente);
                return Results.Ok(result);
            })
            .WithName("GetDocentesCursosByDocente")
            .Produces<List<DocenteCursoDTO>>(StatusCodes.Status200OK)
            .RequireAuthorization("DocentesCursos.Consultar")
            .WithOpenApi();

            app.MapGet("/docentes-cursos/{id:int}", async (int id, IDocenteCursoService service) =>
            {
                var dto = await service.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetDocenteCurso")
            .Produces<DocenteCursoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization("DocentesCursos.Consultar")
            .WithOpenApi();

            app.MapGet("/docentes-cursos", async (IDocenteCursoService service) =>
            {
                var dtos = await service.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllDocentesCursos")
            .Produces<List<DocenteCursoDTO>>(StatusCodes.Status200OK)
            .RequireAuthorization("DocentesCursos.Consultar")
            .WithOpenApi();

            app.MapPost("/docentes-cursos", async (DocenteCursoDTO dto, IDocenteCursoService service) =>
            {
                try
                {
                    var created = await service.AddAsync(dto);
                    return Results.Created($"/docentes-cursos/{created.Id}", created);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddDocenteCurso")
            .Produces<DocenteCursoDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization("DocentesCursos.Alta")
            .WithOpenApi();

            app.MapPut("/docentes-cursos", async (DocenteCursoDTO dto, IDocenteCursoService service) =>
            {
                try
                {
                    var found = await service.UpdateAsync(dto);

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
            .WithName("UpdateDocenteCurso")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization("DocentesCursos.Modificar")
            .WithOpenApi();

            app.MapDelete("/docentes-cursos/{id:int}", async (int id, IDocenteCursoService service) =>
            {
                var deleted = await service.DeleteAsync(id);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeleteDocenteCurso")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization("DocentesCursos.Baja")
            .WithOpenApi();
        }
    }
}
