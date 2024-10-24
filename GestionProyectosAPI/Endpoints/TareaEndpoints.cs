﻿using GestionProyectosAPI.DTOs;
using GestionProyectosAPI.Services.Tarea;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace GestionProyectosAPI.Endpoints
{
    public static class TareaEndpoints
    {
        public static void Add(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/tareas").WithTags("tareas");

            group.MapGet("/", async (ITareaServices tareaServices) =>
            {
                var tareas = await tareaServices.GetTareas();
                // 200 OK: La solicitud se realizo correctamente
                // y devuelve la lista de tareas
                return Results.Ok(tareas);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener productos",
                Description = "Muestra una lsita de toda las tareas"
            }).RequireAuthorization();

            group.MapGet("/{id}", async (int id, ITareaServices tareaServices) =>
            {
                var tarea = await tareaServices.GetTarea(id);
                if (tarea == null)
                    return Results.NotFound();// 404 not found: resurso solicitado no existe
                else
                    return Results.Ok(tarea);// 200 OK: La solicitud se realizo correctamente y de vuelve una tarea

            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener tarea",
                Description = "BUsca un producto por un id"
            }).RequireAuthorization();

            group.MapPost("/", async (TareaRequest tarea, ITareaServices tareaServices) =>
            {
                if (tarea == null)
                    return Results.BadRequest(); // 404 Bad Request: La solicitud no se pudo procesar, error de formato

              var id =  await tareaServices.PostTarea(tarea);
                // 201 Create : el recurso se creo con exito, se devuelve la ubicacion del recusrso creado
                return Results.Created($"api/tareas/{id}",tarea); 
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "crear tarea",
                Description = "Crear un nuevo tarea"
            }).RequireAuthorization();

            group.MapPut("/{id}", async (int id ,TareaRequest tarea, ITareaServices tareaServices) =>
            {
                var result = await tareaServices.PutTarea(id, tarea);
                if (result == -1)
                    return Results.NotFound();// 404 not found: resurso solicitado no existe
                else
                    return Results.Ok(tarea);// 200 OK: La solicitud se realizo correctamente y de vuelve una tarea
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Modificar tarea",
                Description = "Actualiza una tarea existente"
            }).RequireAuthorization();

            group.MapDelete("/{id}", async (int id,  ITareaServices tareaServices) =>
            {
                var result = await tareaServices.DeleteTarea(id);
                if (result == -1)
                    return Results.NotFound();// 404 not found: resurso solicitado no existe
                else
                    return Results.NoContent();// 204 NO Content: Recuros eliminado
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "ELiminar tarea",
                Description = "ELiminar una tarea existente"
            }).RequireAuthorization();

            // Nuevo endpoint para obtener tareas con paginación
            group.MapGet("/paginacion", async (int numeroPagina, ITareaServices tareaServices) =>
            {
                var paginacionRequest = new PaginacionRequest
                {
                    NumeroPagina = numeroPagina
                };

                var tareasPaginadas = await tareaServices.ObtenerTareasPaginadas(paginacionRequest);
                return Results.Ok(tareasPaginadas); // 200 OK con la lista paginada de tareas
            })
            .WithOpenApi(o => new OpenApiOperation(o)
            {
             Summary = "Obtener tareas paginadas",
             Description = "Obtiene una lista paginada de tareas"
            })
            .RequireAuthorization();
        }
    }
}
