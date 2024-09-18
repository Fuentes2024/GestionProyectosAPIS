using GestionProyectosAPI.DTOs;
using GestionProyectosAPI.Services.Rol;
using GestionProyectosAPI.Services.Tarea;
using Microsoft.OpenApi.Models;

namespace GestionProyectosAPI.Endpoints
{
    public static class RolEndpoints
    {
        public static void Add(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/roles").WithTags("roles");

            group.MapGet("/", async (IRolServices rolServices) =>
            {
                var rols =await rolServices.GetRols();
                // 200 OK: La solicitud se realizo correctamente
                // y devuelve la lista de tareas
                return Results.Ok(rols);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener roles",
                Description = "Muestra una lista de todos las roles"
            });

            group.MapGet("/{id}", async (int id, IRolServices rolServices) =>
            {
                var rol = await rolServices.GetRol(id);
                if (rol == null)
                    return Results.NotFound();// 404 not found: resurso solicitado no existe
                else
                    return Results.Ok(rol);// 200 OK: La solicitud se realizo correctamente y de vuelve una tarea

            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener rol",
                Description = "BUsca un rol por un id"
            });

            group.MapPost("/", async (RolRequest rol, IRolServices rolServices) =>
            {
                if (rol == null) {
                    return Results.BadRequest(); // 404 Bad Request: La solicitud no se pudo procesar, error de formato
                }

                var id = await rolServices.PostRol(rol);
                // 201 Create : el recurso se creo con exito, se devuelve la ubicacion del recusrso creado
                return Results.Created($"api/roles/{id}", rol);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "crear rol",
                Description = "Crear un nuevo rol"
            });

            group.MapPut("/{id}", async (int id, RolRequest rol, IRolServices rolServices) =>
            {
                var result = await rolServices.PutRol(id, rol);
                if (result == -1)
                    return Results.NotFound();// 404 not found: resurso solicitado no existe
                else
                    return Results.Ok(rol);// 200 OK: La solicitud se realizo correctamente y de vuelve una tarea
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Modificar rol",
                Description = "Actualiza un rol existente"
            }).RequireAuthorization();

            group.MapDelete("/{id}", async (int id, IRolServices rolServices) =>
            {
                var result = await rolServices.DeleteRol(id);
                if (result == -1)
                    return Results.NotFound();// 404 not found: resurso solicitado no existe
                else
                    return Results.NoContent();// 204 NO Content: Recuros eliminado
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "ELiminar rol",
                Description = "ELiminar un rol existente"
            }).RequireAuthorization();
        }
    }
}
