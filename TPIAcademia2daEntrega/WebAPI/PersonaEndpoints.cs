using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class PersonaEndpoints
    {
        public static void MapPersonaEndpoints(this WebApplication app)
        {
            app.MapGet("/personas/{id}", async (int id, IPersonaService personaService) =>
            {
                PersonaDTO? dto = await personaService.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetPersona")
            .Produces<PersonaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/personas", async (IPersonaService personaService) =>
            {
                var dtos = await personaService.GetAllAsync();

                return Results.Ok(dtos);
            })
            .WithName("GetAllPersonas")
            .Produces<List<PersonaDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/personas", async (PersonaDTO dto, IPersonaService personaService) =>
            {
                try
                {
                    PersonaDTO personaDTO = await personaService.AddAsync(dto);

                    return Results.Created($"/personas/{personaDTO.Id}", personaDTO);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddPersona")
            .Produces<PersonaDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPut("/personas", async (PersonaDTO dto, IPersonaService personaService) =>
            {
                try
                {
                    var found = await personaService.UpdateAsync(dto);

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
            .WithName("UpdatePersona")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/personas/{id}", async (int id, IPersonaService personaService) =>
            {
                var deleted = await personaService.DeleteAsync(id);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeletePersona")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/personas/criteria", async (string texto, IPersonaService personaService) =>
            {
                try
                {
                    var criteria = new PersonaCriteriaDTO { Texto = texto };
                    var personas = await personaService.GetByCriteriaAsync(criteria);
                    return Results.Ok(personas);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("GetPersonasByCriteria")
            .WithOpenApi();
        }
    }
}
