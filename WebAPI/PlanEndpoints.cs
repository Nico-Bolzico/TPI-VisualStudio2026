using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class PlanEndpoints
    {
        public static void MapPlanEndpoints(this WebApplication app)
        {
            app.MapGet("/planes/criteria", async (string texto, IPlanService planService) =>
            {
                try
                {
                    var criteria = new PlanCriteriaDTO { Texto = texto };
                    var planes = await planService.GetByCriteriaAsync(criteria);
                    return Results.Ok(planes);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("GetPlanesByCriteria")
            .Produces<List<PlanDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization("Planes.Consultar")
            .WithOpenApi();

            app.MapGet("/planes/{id:int}", async (int id, IPlanService planService) =>
            {
                PlanDTO? dto = await planService.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetPlan")
            .Produces<PlanDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization("Planes.Consultar")
            .WithOpenApi();

            app.MapGet("/planes", async (IPlanService planService) =>
            {
                var dtos = await planService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllPlanes")
            .Produces<List<PlanDTO>>(StatusCodes.Status200OK)
            .RequireAuthorization("Planes.Consultar")
            .WithOpenApi();

            app.MapPost("/planes", async (PlanDTO dto, IPlanService planService) =>
            {
                try
                {
                    PlanDTO planDTO = await planService.AddAsync(dto);
                    return Results.Created($"/planes/{planDTO.Id}", planDTO);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddPlan")
            .Produces<PlanDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization("Planes.Alta")
            .WithOpenApi();

            app.MapPut("/planes", async (PlanDTO dto, IPlanService planService) =>
            {
                try
                {
                    var found = await planService.UpdateAsync(dto);

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
            .WithName("UpdatePlan")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization("Planes.Modificar")
            .WithOpenApi();

            app.MapDelete("/planes/{id:int}", async (int id, IPlanService planService) =>
            {
                var deleted = await planService.DeleteAsync(id);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeletePlan")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization("Planes.Baja")
            .WithOpenApi();
        }
    }
}
