using AutoMapper;
using GestionProyectosAPI.DTOs;
using GestionProyectosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

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
            var proyecto = await _db.Proyectos.ToListAsync();
            var proyectoList = _mapper.Map<List<Proyectos>, List<ProyectoResponse>>(proyecto);

            return proyectoList;
        }

        public async Task<int> PostProyecto(ProyectoRequest proyecto)
        {
            var proyectoRequest = _mapper.Map<ProyectoRequest, Proyectos>(proyecto);
            await _db.Proyectos.AddAsync(proyectoRequest);

            return await _db.SaveChangesAsync();
        }
        public async Task<int> putProyecto(int proyectoId, ProyectoRequest proyecto)
        {
            var entity = await _db.Proyectos.FindAsync(proyectoId);
            if (entity == null)
                return -1;

            entity.Nombre = proyecto.Nombre;
            entity.Descripcion = proyecto.Descripcion;
            entity.FechaInicio = proyecto.FechaInicio;
            entity.FechaFin = proyecto.FechaFin;
            entity.UsuarioId = proyecto.UsuarioId;

            return _db.SaveChanges();
        }
    }
}
