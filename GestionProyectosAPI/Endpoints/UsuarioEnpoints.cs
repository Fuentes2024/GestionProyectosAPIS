using GestionProyectosAPI.DTOs;
using GestionProyectosAPI.Services.Tarea;
using GestionProyectosAPI.Services.Usuario;
using Microsoft.OpenApi.Models;

namespace GestionProyectosAPI.Endpoints
{
    public static class UsuarioEnpoints
    {
        public static void Add(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/usuarios").WithTags("usuarios");
            group.MapGet("/", async (IUsuarioServices usuarioServices) =>
            {
                var usuarios = await usuarioServices.GetUsuarios();
                // 200 OK: La solicitud se realizo correctamente
                // y devuelve la lista de tareas
                return Results.Ok(usuarios);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener usuarios",
                Description = "Muestra una lsita de todos los usuarios"
            });

            group.MapGet("/{id}", async (int id, IUsuarioServices usuarioServices) =>
            {
                var usuario = await usuarioServices.GetUsuario(id);
                if (usuario == null)
                    return Results.NotFound();// 404 not found: resurso solicitado no existe
                else
                    return Results.Ok(usuario);// 200 OK: La solicitud se realizo correctamente y de vuelve una tarea

            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener usuario",
                Description = "BUsca un usuario por un id"
            });

            group.MapPost("/", async (UsuarioRequest usuario, IUsuarioServices usuarioServices) =>
            {
                if (usuario == null)
                    return Results.BadRequest(); // 404 Bad Request: La solicitud no se pudo procesar, error de formato

              var id =  await usuarioServices.PostUsuario(usuario);
                // 201 Create : el recurso se creo con exito, se devuelve la ubicacion del recusrso creado
                return Results.Created($"api/usuarios/{id}",usuario); 
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "crear usuario",
                Description = "Crear un nuevo usuario"
            });

            group.MapPut("/{id}", async (int id, UsuarioRequest usuario, IUsuarioServices usuarioServices) =>
            {
                var result = await usuarioServices.PutUsuario(id, usuario);
                if (result == -1)
                    return Results.NotFound();// 404 not found: resurso solicitado no existe
                else
                    return Results.Ok(usuario);// 200 OK: La solicitud se realizo correctamente y de vuelve una tarea
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Modificar usuario",
                Description = "Actualiza un usuario existente"
            });

            group.MapDelete("/{id}", async (int id, IUsuarioServices usuarioServices) =>
            {
                var result = await usuarioServices.DeleteUsuario(id);
                if (result == -1)
                    return Results.NotFound();// 404 not found: resurso solicitado no existe
                else
                    return Results.NoContent();// 204 NO Content: Recuros eliminado
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "ELiminar usuario",
                Description = "ELiminar un usuario existente"
            });

        }
    }
}
