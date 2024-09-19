using GestionProyectosAPI.DTOs;
using GestionProyectosAPI.Services.Miembro;
using Microsoft.OpenApi.Models;

namespace GestionProyectosAPI.Endpoints
{
    public static class MiembroEndpoints
    {
        public static void Add(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/miembros").WithTags("miembros");

            group.MapGet("/", async (IMiembroServices miembroServices) =>
            {
                var miembro = await miembroServices.GetMiembros();
                // 200 OK: La solicitud se realizo correctamente
                // y devuelve la lista de tareas
                return Results.Ok(miembro);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener Miembro",
                Description = "Muestra una lista de todos los miembros"
            }).RequireAuthorization();

            group.MapGet("/{id}", async (int id, IMiembroServices miembroServices) =>
            {
                var miembro = await miembroServices.GetMiembro(id);
                if (miembro == null)
                    return Results.NotFound();// 404 not found: resurso solicitado no existe
                else
                    return Results.Ok(miembro);// 200 OK: La solicitud se realizo correctamente y de vuelve una tarea

            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener miembro",
                Description = "Busca un miembro por un id"
            }).RequireAuthorization();

            group.MapPost("/", async (MiembroEquipoRequest miembro, IMiembroServices miembroServices) =>
            {
                if (miembro == null)
                    return Results.BadRequest(); // 404 Bad Request: La solicitud no se pudo procesar, error de formato

                var id = await miembroServices.PostMiembro(miembro);
                // 201 Create : el recurso se creo con exito, se devuelve la ubicacion del recusrso creado
                return Results.Created($"api/miembros/{id}", miembro);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "crear miembro",
                Description = "Crear un nuevo miembro"
            }).RequireAuthorization();

            group.MapPut("/{id}", async (int id, MiembroEquipoRequest miembro, IMiembroServices miembroServices) =>
            {
                var result = await miembroServices.PutMiembro(id, miembro);
                if (result == -1)
                    return Results.NotFound();// 404 not found: resurso solicitado no existe
                else
                    return Results.Ok(miembro);// 200 OK: La solicitud se realizo correctamente y de vuelve una tarea
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Modificar miembro",
                Description = "Actualiza un miembro existente"
            }).RequireAuthorization();

            group.MapDelete("/{id}", async (int id, IMiembroServices miembroServices) =>
            {
                var result = await miembroServices.DeleteMiembro(id);
                if (result == -1)
                    return Results.NotFound();// 404 not found: resurso solicitado no existe
                else
                    return Results.NoContent();// 204 NO Content: Recuros eliminado
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "ELiminar miembro",
                Description = "ELiminar un miembro existente"
            }).RequireAuthorization();
        }
    }
}

