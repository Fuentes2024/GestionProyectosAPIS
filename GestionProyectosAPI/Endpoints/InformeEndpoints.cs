using GestionProyectosAPI.DTOs;
using GestionProyectosAPI.Services.Informe;
using Microsoft.OpenApi.Models;

namespace GestionProyectosAPI.Endpoints
{
    public static class InformeEndpoints
    {
        public static void Add(this IEndpointRouteBuilder routes) {
            var group = routes.MapGroup("/api/informes").WithTags("informes");

            group.MapGet("/", async (IInformeServices informeServices) =>
            {
                var informes = await informeServices.GetInformes();
                //200 ok: La solicitud se a realizado corretamente
                //y devuelve la lista de informes
                return Results.Ok(informes);
            }).WithOpenApi(o=> new OpenApiOperation(o) {
                Summary = "Obtener Informes",
                Description = "Muestra una lista de todos los informes."
            }).RequireAuthorization();

            group.MapGet("/{id}", async (int id, IInformeServices informeServices) =>
            {
                var informe = await informeServices.GetInforme(id);
                if (informe == null)
                    return Results.NotFound();//404 not found:El recurso solicitado no existe
                else
                    return Results.Ok(informe);//200 ok: La solicitud se a realizado corretamente y devuelve la lista de informes
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener Informe",
                Description = "Buscar un Informe por id."
            }).RequireAuthorization();

            group.MapPost("/", async (InformeRequest informe, IInformeServices informeServices) =>
            {
                if (informe == null)
                    return Results.BadRequest();// 400 BadRequest: La solicitud no se puede procesar, Error de formato

                var id = await informeServices.PostInforme(informe);
                //201 Created: El recurso se creo con exito, se devuelve la ubicacion del recurso creado 
                return Results.Created($"api/informe/{id}", informe);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Crear Informes",
                Description = "Crear un nuevo  Informe."
            }).RequireAuthorization();

            group.MapPut("/{id}", async (int id, InformeRequest informe, IInformeServices informeServices) =>
            {
                var result = await informeServices.PutInforme(id, informe);

                if(result == -1)
                    return Results.NotFound();//404 not found:El recurso solicitado no existe
                 else
                    return Results.Ok(result);//200 ok: La solicitud se a realizado corretamente y devuelve la lista de informes
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Modificar Informes",
                Description = "Actualizar un Informe exisitente."
            }).RequireAuthorization();


            group.MapDelete("/{id}", async (int id, IInformeServices informeServices) =>
            {
                var result = await informeServices.DeleteInforme(id);

                if (result == -1)
                    return Results.NotFound();//404 not found:El recurso solicitado no existe.
                 else
                    return Results.NoContent();//202 NoContent: Recurso eliminado
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Eliminar Informes",
                Description = "Eliminar un Informe exisitente."
            }).RequireAuthorization();
        }

         
    }
}
