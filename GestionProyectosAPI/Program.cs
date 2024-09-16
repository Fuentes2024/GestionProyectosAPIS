using GestionProyectosAPI.DTOs;
using GestionProyectosAPI.Endpoints;
using GestionProyectosAPI.Models;
using GestionProyectosAPI.Services.Miembro;
using GestionProyectosAPI.Services.Tarea;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BbContext>(
    o=>o.UseSqlServer(builder.Configuration.GetConnectionString("BbConnection"))
    );

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<ITareaServices, TareaServices>();
builder.Services.AddScoped<IMiembroServices, MiembroServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseEndpoints();

app.Run();