using GestionProyectosAPI.DTOs;
using GestionProyectosAPI.Endpoints;
using GestionProyectosAPI.Models;
using GestionProyectosAPI.Services.Informe;
using GestionProyectosAPI.Services.Miembro;
using GestionProyectosAPI.Services.Proyecto;
using GestionProyectosAPI.Services.Rol;
using GestionProyectosAPI.Services.Tarea;
using GestionProyectosAPI.Services.Usuario;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT en el siguiente formato: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer" // Asegúrate de que este ID coincida con el ID usado en AddSecurityDefinition
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddDbContext<BbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("BbConnection"))
);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<ITareaServices, TareaServices>();
builder.Services.AddScoped<IMiembroServices, MiembroServices>();
builder.Services.AddScoped<IRolServices, RolServices>();
builder.Services.AddScoped<IProyectoServices, ProyectoServices>();
builder.Services.AddScoped<IUsuarioServices, UsuarioServices>();
builder.Services.AddScoped<IInformeServices, InformeServices>();

var jwtSetting = builder.Configuration.GetSection("JwtSetting");
var secretKey = jwtSetting.GetValue<string>("SecretKey");

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    // Esquema por defecto
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    // Permite usar HTTP en lugar de HTTPS
    options.RequireHttpsMetadata = false;
    // Guardar el token en el contexto de autenticación
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSetting.GetValue<string>("Isusuario"), // Corregido aquí
        ValidAudience = jwtSetting.GetValue<string>("Audience"),
        // CONVIERTE UNA CADENA DE TEXTO A BYTE
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Use the endpoints with a proper method like app.MapEndpoints() if necessary
app.UseEndpoints();
// Corregido aquí, usa app.MapEndpoints() en lugar de app.UseEndpoints()

app.Run();
