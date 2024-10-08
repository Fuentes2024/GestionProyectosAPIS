﻿using AutoMapper;
using GestionProyectosAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using GestionProyectosAPI.Models;

namespace GestionProyectosAPI.Services.Proyecto
{
    public class ProyectoServices : IProyectoServices
    {
        private readonly BbContext _db;
        private readonly IMapper _mapper;

        public ProyectoServices(BbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> DeleteProyecto(int proyectoId)
        {
            var proyecto = await _db.Proyectos.FindAsync(proyectoId);
            if (proyecto == null)
                return -1;

            _db.Proyectos.Remove(proyecto);
            return await _db.SaveChangesAsync();
        }

        public async Task<ProyectoResponse> GetProyecto(int proyectoId)
        {
            var proyecto = await _db.Proyectos.FindAsync(proyectoId);
            var proyectoResponse = _mapper.Map<Proyectos, ProyectoResponse>(proyecto);
            return proyectoResponse;
        }

        public async Task<List<ProyectoResponse>> GetProyectos()
        {
            var proyectos = await _db.Proyectos.ToListAsync();
            var proyectosList = _mapper.Map<List<Proyectos>, List<ProyectoResponse>>(proyectos);
            return proyectosList;
        }

        public async Task<int> PostProyecto(ProyectoRequest proyecto)
        {
            var proyectoRequest = _mapper.Map<ProyectoRequest, Proyectos>(proyecto);
            await _db.Proyectos.AddAsync(proyectoRequest);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> PutProyecto(int proyectoId, ProyectoRequest proyecto)
        {
            var entity = await _db.Proyectos.FindAsync(proyectoId);
            if (entity == null)
                return -1;

            entity.Nombre = proyecto.Nombre;
            entity.Descripcion = proyecto.Descripcion;
            entity.FechaInicio = proyecto.FechaInicio;
            entity.FechaFin = proyecto.FechaFin;
            entity.UsuarioId = proyecto.UsuarioId;

            _db.Proyectos.Update(entity);
            return await _db.SaveChangesAsync();
        }
    }
}
