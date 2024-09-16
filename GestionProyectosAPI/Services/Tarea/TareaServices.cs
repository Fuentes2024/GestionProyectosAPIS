using AutoMapper;
using GestionProyectosAPI.DTOs;
using GestionProyectosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectosAPI.Services.Tarea 
{
    public class TareaServices : ITareaServices
    {
        private readonly BbContext _db;
        private readonly IMapper _mapper;

        public TareaServices(BbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> DeleteTarea(int tareaId)
        {
            var tarea = await _db.Tareas.FindAsync(tareaId);
            if (tarea == null)
                return -1;

            _db.Tareas.Remove(tarea);

            return await _db.SaveChangesAsync();
        }

        public async Task<TareaResponse> GetTarea(int tareaId)
        {
            var tarea = await _db.Tareas.FindAsync(tareaId);
            var tareaResponse = _mapper.Map<Tareas,TareaResponse>(tarea);

            return tareaResponse;
        }

        public async Task<List<TareaResponse>> GetTareas()
        {
            var tareas = await _db.Tareas.ToListAsync();
            var tareaList = _mapper.Map<List<Tareas>, List<TareaResponse>>(tareas);

            return tareaList;

        }

        public async Task<int> PostTarea(TareaRequest tarea)
        {
            var tareaRequest = _mapper.Map<TareaRequest, Tareas>(tarea);
            await _db.Tareas.AddAsync(tareaRequest);
            await _db.SaveChangesAsync();
            return tareaRequest.TareaId;

           
        }

        public async Task<int> PutTarea(int tareaId, TareaRequest tarea)
        {
            var entity = await _db.Tareas.FindAsync(tareaId);
            if (entity == null)
                return -1;

            entity.Nombre = tarea.Nombre;
            entity.Descripcion = tarea.Descripcion;
            entity.EstadoTarea = tarea.EstadoTarea;
            entity.FechaInicio = tarea.FechaInicio;
            entity.FechaFin = tarea.FechaFin;
            entity.Prioridad = tarea.Prioridad;
            entity.MiembroEquipoId = tarea.MiembroEquipoId;
            entity.ProyectoId = tarea.ProyectoId;

            return _db.SaveChanges();
        }
    }
}
