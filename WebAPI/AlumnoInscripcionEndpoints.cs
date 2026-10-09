using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class AlumnoInscripcionEndpoints
    {
        public static void MapAlumnoInscripcionEndpoints(this WebApplication app)
        {
            app.MapGet("/inscripciones/curso/{idCurso:int}", async (int idCurso, IAlumnoInscripcionService inscripcionService) =>
            {
                var result = await inscripcionService.GetByCursoAsync(idCurso);
                return Results.Ok(result);
            })
            .WithName("GetInscripcionesByCurso")
            .Produces<List<AlumnoInscripcionDTO>>(StatusCodes.Status200OK)
            .RequireAuthorization("Inscripciones.Consultar")
            .WithOpenApi();

            app.MapGet("/inscripciones/alumno/{idAlumno:int}", async (int idAlumno, IAlumnoInscripcionService inscripcionService) =>
            {
                var result = await inscripcionService.GetByAlumnoAsync(idAlumno);
                return Results.Ok(result);
            })
            .WithName("GetInscripcionesByAlumno")
            .Produces<List<AlumnoInscripcionDTO>>(StatusCodes.Status200OK)
            .RequireAuthorization("Inscripciones.Consultar")
            .WithOpenApi();

            app.MapGet("/inscripciones/{id:int}", async (int id, IAlumnoInscripcionService inscripcionService) =>
            {
                var dto = await inscripcionService.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetInscripcion")
            .Produces<AlumnoInscripcionDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization("Inscripciones.Consultar")
            .WithOpenApi();

            app.MapGet("/inscripciones", async (IAlumnoInscripcionService inscripcionService) =>
            {
                var dtos = await inscripcionService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllInscripciones")
            .Produces<List<AlumnoInscripcionDTO>>(StatusCodes.Status200OK)
            .RequireAuthorization("Inscripciones.Consultar")
            .WithOpenApi();

            app.MapPost("/inscripciones", async (AlumnoInscripcionDTO dto, IAlumnoInscripcionService inscripcionService) =>
            {
                try
                {
                    var created = await inscripcionService.AddAsync(dto);
                    return Results.Created($"/inscripciones/{created.Id}", created);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (InvalidOperationException ex)
                {
                    return Results.Conflict(new { error = ex.Message });
                }
            })
            .WithName("AddInscripcion")
            .Produces<AlumnoInscripcionDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .RequireAuthorization("Inscripciones.Alta")
            .WithOpenApi();

            app.MapPut("/inscripciones", async (AlumnoInscripcionDTO dto, IAlumnoInscripcionService inscripcionService) =>
            {
                try
                {
                    var found = await inscripcionService.UpdateAsync(dto);

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
            .WithName("UpdateInscripcion")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization("Inscripciones.Modificar")
            .WithOpenApi();

            app.MapDelete("/inscripciones/{id:int}", async (int id, IAlumnoInscripcionService inscripcionService) =>
            {
                var deleted = await inscripcionService.DeleteAsync(id);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeleteInscripcion")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization("Inscripciones.Baja")
            .WithOpenApi();
        }
    }
}
