using GestionProyectosAPI.DTOs;
using GestionProyectosAPI.Services.Tarea;
using GestionProyectosAPI.Services.Usuario;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            }).RequireAuthorization();

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

                var id = await usuarioServices.PostUsuario(usuario);
                // 201 Create : el recurso se creo con exito, se devuelve la ubicacion del recusrso creado
                return Results.Created($"api/usuarios/{id}", usuario);
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
            }).RequireAuthorization();

            group.MapDelete("/{id}", async (int id, IUsuarioServices usuarioServices) =>
            {
                var result = await usuarioServices.DeleteUsuario(id);
                if (result == -1)
                    return Results.NotFound();// 404 not found: resurso solicitado no existe
                else
                    return Results.Ok(new { message = "El registro ha sido eliminado correctamente." }); // 200 OK con mensaje
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "ELiminar usuario",
                Description = "ELiminar un usuario existente"
            }).RequireAuthorization();

            group.MapPost("/Login", async (UsuarioRequest usuario, IUsuarioServices usuarioServices, IConfiguration config) =>
            {
                // Verifica las credenciales del usuario
                var login = await usuarioServices.Login(usuario);

                // Si el login es nulo, las credenciales son incorrectas
                 if (login is null)
                {
                    return Results.Unauthorized(); // Retorna el estado 401: Unauthorized
                }
                else
                {
                    // Cargar configuraciones JWT desde appsettings
                    var jwtSetting = config.GetSection("JwtSetting");
                    var secretKey = jwtSetting.GetValue<string>("SecretKey");
                    var issuer = jwtSetting.GetValue<string>("Isusuario");
                    var audience = jwtSetting.GetValue<string>("Audience");

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(secretKey);

                    // Crear el descriptor del token
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                new Claim(ClaimTypes.Email, login.Correo), 
                new Claim(ClaimTypes.Name, login.Nombre)
               
            }),
                        Expires = DateTime.UtcNow.AddHours(1), // El token expira en 1 hora
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = new SigningCredentials(
                            new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256Signature)
                    };

                    // Crear el token JWT
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwt = tokenHandler.WriteToken(token); // Convertir el token a una cadena

                    // Retornar el token
                    return Results.Content(jwt);
                }
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Login usuario",
                Description = "Generar un token para inicio de sesión"
            });         

        }
    }
}

