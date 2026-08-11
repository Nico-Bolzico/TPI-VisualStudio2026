using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class MateriaEndpoints
    {
        public static void MapMateriaEndpoints(this WebApplication app)
        {
            app.MapGet("/materias/{id}", async (int id, IMateriaService materiaService) =>
            {
                MateriaDTO? dto = await materiaService.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetMateria")
            .Produces<MateriaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/materias", async (IMateriaService materiaService) =>
            {
                var dtos = await materiaService.GetAllAsync();

                return Results.Ok(dtos);
            })
            .WithName("GetAllMaterias")
            .Produces<List<MateriaDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/materias", async (MateriaDTO dto, IMateriaService materiaService) =>
            {
                try
                {
                    MateriaDTO materiaDTO = await materiaService.AddAsync(dto);

                    return Results.Created($"/materias/{materiaDTO.Id}", materiaDTO);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddMateria")
            .Produces<MateriaDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPut("/materias", async (MateriaDTO dto, IMateriaService materiaService) =>
            {
                try
                {
                    var found = await materiaService.UpdateAsync(dto);

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
            .WithName("UpdateMateria")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/materias/{id}", async (int id, IMateriaService materiaService) =>
            {
                var deleted = await materiaService.DeleteAsync(id);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeleteMateria")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/materias/criteria", async (string texto, IMateriaService materiaService) =>
            {
                try
                {
                    var criteria = new MateriaCriteriaDTO { Texto = texto };
                    var materias = await materiaService.GetByCriteriaAsync(criteria);
                    return Results.Ok(materias);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("GetMateriasByCriteria")
            .WithOpenApi();
        }
    }
}
