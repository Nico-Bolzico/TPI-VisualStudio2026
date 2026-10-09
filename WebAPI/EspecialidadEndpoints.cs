using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class EspecialidadEndpoints
    {
        public static void MapEspecialidadEndpoints(this WebApplication app)
        {
            app.MapGet("/especialidades/criteria", async (string texto, IEspecialidadService especialidadService) =>
            {
                try
                {
                    var criteria = new EspecialidadCriteriaDTO { Texto = texto };
                    var result = await especialidadService.GetByCriteriaAsync(criteria);
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("GetEspecialidadesByCriteria")
            .Produces<List<EspecialidadDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization("Especialidades.Consultar")
            .WithOpenApi();

            app.MapGet("/especialidades/{id:int}", async (int id, IEspecialidadService especialidadService) =>
            {
                var dto = await especialidadService.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetEspecialidad")
            .Produces<EspecialidadDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization("Especialidades.Consultar")
            .WithOpenApi();

            app.MapGet("/especialidades", async (IEspecialidadService especialidadService) =>
            {
                var dtos = await especialidadService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllEspecialidades")
            .Produces<List<EspecialidadDTO>>(StatusCodes.Status200OK)
            .RequireAuthorization("Especialidades.Consultar")
            .WithOpenApi();

            app.MapPost("/especialidades", async (EspecialidadDTO dto, IEspecialidadService especialidadService) =>
            {
                try
                {
                    var created = await especialidadService.AddAsync(dto);
                    return Results.Created($"/especialidades/{created.Id}", created);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddEspecialidad")
            .Produces<EspecialidadDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization("Especialidades.Alta")
            .WithOpenApi();

            app.MapPut("/especialidades", async (EspecialidadDTO dto, IEspecialidadService especialidadService) =>
            {
                try
                {
                    var found = await especialidadService.UpdateAsync(dto);

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
            .WithName("UpdateEspecialidad")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization("Especialidades.Modificar")
            .WithOpenApi();

            app.MapDelete("/especialidades/{id:int}", async (int id, IEspecialidadService especialidadService) =>
            {
                var deleted = await especialidadService.DeleteAsync(id);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeleteEspecialidad")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization("Especialidades.Baja")
            .WithOpenApi();
        }
    }
}
