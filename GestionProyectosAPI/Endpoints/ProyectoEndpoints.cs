using GestionProyectosAPI.DTOs;
using GestionProyectosAPI.Services.Proyecto;
using GestionProyectosAPI.Services.Tarea;
using Microsoft.OpenApi.Models;

namespace GestionProyectosAPI.Endpoints
{
    public static class ProyectoEndpoints
    {
        public static void Add(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/proyectos").WithTags("proyectos");

            group.MapGet("/", async (IProyectoServices proyectoServices) =>
            {
                var proyectos = await proyectoServices.GetProyectos();
                // 200 OK: La solicitud se realizo correctamente
                // y devuelve la lista de tareas
                return Results.Ok(proyectos);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener proyectos",
                Description = "Muestra una lista de todos los proyectos"
            });

            group.MapGet("/{id}", async (int id, IProyectoServices proyectoServices) =>
            {
                var proyecto = await proyectoServices.GetProyecto(id);
                if (proyecto == null)
                    return Results.NotFound();// 404 not found: resurso solicitado no existe
                else
                    return Results.Ok(proyecto);// 200 OK: La solicitud se realizo correctamente y de vuelve una tarea

            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener proyecto",
                Description = "Busca un proyecto por un id"
            });

            group.MapPost("/", async (ProyectoRequest proyecto, IProyectoServices proyectoServices) =>
            {
                if (proyecto == null)
                    return Results.BadRequest(); // 404 Bad Request: La solicitud no se pudo procesar, error de formato

                var id = await proyectoServices.PostProyecto(proyecto);
                // 201 Create : el recurso se creo con exito, se devuelve la ubicacion del recusrso creado
                return Results.Created($"api/proyectos/{id}", proyecto);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "crear proyecto",
                Description = "Crear un nuevo proyecto"
            });

            group.MapPut("/{id}", async (int id, ProyectoRequest proyecto, IProyectoServices proyectoServices) =>
            {
                var result = await proyectoServices.PutProyecto(id, proyecto);
                if (result == -1)
                    return Results.NotFound();// 404 not found: resurso solicitado no existe
                else
                    return Results.Ok(proyecto);// 200 OK: La solicitud se realizo correctamente y de vuelve una tarea
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Modificar proyecto",
                Description = "Actualiza un proyecto existente"
            });

            group.MapDelete("/{id}", async (int id, IProyectoServices proyectoServices) =>
            {
                var result = await proyectoServices.DeleteProyecto(id);
                if (result == -1)
                    return Results.NotFound();// 404 not found: resurso solicitado no existe
                else
                    return Results.NoContent();// 204 NO Content: Recuros eliminado
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "ELiminar proyecto",
                Description = "ELiminar un proyecto existente"
            });
        }
    }
}
