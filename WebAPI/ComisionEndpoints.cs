using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class ComisionEndpoints
    {
        public static void MapComisionEndpoints(this WebApplication app)
        {
            app.MapGet("/comisiones/criteria", async (string texto, IComisionService comisionService) =>
            {
                try
                {
                    var criteria = new ComisionCriteriaDTO { Texto = texto };
                    var result = await comisionService.GetByCriteriaAsync(criteria);
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("GetComisionesByCriteria")
            .Produces<List<ComisionDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization("Comisiones.Consultar")
            .WithOpenApi();

            app.MapGet("/comisiones/{id:int}", async (int id, IComisionService comisionService) =>
            {
                var dto = await comisionService.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetComision")
            .Produces<ComisionDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization("Comisiones.Consultar")
            .WithOpenApi();

            app.MapGet("/comisiones", async (IComisionService comisionService) =>
            {
                var dtos = await comisionService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllComisiones")
            .Produces<List<ComisionDTO>>(StatusCodes.Status200OK)
            .RequireAuthorization("Comisiones.Consultar")
            .WithOpenApi();

            app.MapPost("/comisiones", async (ComisionDTO dto, IComisionService comisionService) =>
            {
                try
                {
                    var created = await comisionService.AddAsync(dto);
                    return Results.Created($"/comisiones/{created.Id}", created);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddComision")
            .Produces<ComisionDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization("Comisiones.Alta")
            .WithOpenApi();

            app.MapPut("/comisiones", async (ComisionDTO dto, IComisionService comisionService) =>
            {
                try
                {
                    var found = await comisionService.UpdateAsync(dto);

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
            .WithName("UpdateComision")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization("Comisiones.Modificar")
            .WithOpenApi();

            app.MapDelete("/comisiones/{id:int}", async (int id, IComisionService comisionService) =>
            {
                var deleted = await comisionService.DeleteAsync(id);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeleteComision")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization("Comisiones.Baja")
            .WithOpenApi();
        }
    }
}
