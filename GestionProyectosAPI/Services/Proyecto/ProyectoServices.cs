using AutoMapper;
using GestionProyectosAPI.DTOs;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
using GestionProyectosAPI.Models;



=======
using GestionProyectosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
>>>>>>> ELI

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
<<<<<<< HEAD
            if (proyecto == null) 
            return  -1;
=======
            if (proyecto == null)
                return -1;
>>>>>>> ELI

            _db.Proyectos.Remove(proyecto);

            return await _db.SaveChangesAsync();
<<<<<<< HEAD
                
=======

>>>>>>> ELI
        }

        public async Task<ProyectoResponse> GetProyecto(int proyectoId)
        {
            var proyecto = await _db.Proyectos.FindAsync(proyectoId);
            var proyectoResponse = _mapper.Map<Proyectos, ProyectoResponse>(proyecto);
<<<<<<< HEAD
            return proyectoResponse;
        }


        public async Task<List<ProyectoResponse>> GetProyectos()
        {
            var proyectos = await _db.Proyectos.ToListAsync();
            var proyectosList = _mapper.Map <List<Proyectos>, List<ProyectoResponse>>(proyectos); 

            return proyectosList;

=======

            return proyectoResponse;
        }

        public async Task<List<ProyectoResponse>> GetProyectos()
        {
            var proyecto = await _db.Proyectos.ToListAsync();
            var proyectoList = _mapper.Map<List<Proyectos>, List<ProyectoResponse>>(proyecto);

            return proyectoList;
>>>>>>> ELI
        }

        public async Task<int> PostProyecto(ProyectoRequest proyecto)
        {
            var proyectoRequest = _mapper.Map<ProyectoRequest, Proyectos>(proyecto);
            await _db.Proyectos.AddAsync(proyectoRequest);

            return await _db.SaveChangesAsync();
<<<<<<< HEAD

        }

        public async Task<int> PutProyecto(int proyectoId, ProyectoRequest proyecto)
=======
        }
        public async Task<int> putProyecto(int proyectoId, ProyectoRequest proyecto)
>>>>>>> ELI
        {
            var entity = await _db.Proyectos.FindAsync(proyectoId);
            if (entity == null)
                return -1;

            entity.Nombre = proyecto.Nombre;
            entity.Descripcion = proyecto.Descripcion;
            entity.FechaInicio = proyecto.FechaInicio;
            entity.FechaFin = proyecto.FechaFin;
            entity.UsuarioId = proyecto.UsuarioId;

<<<<<<< HEAD
            _db.Proyectos.Update(entity);

            return await _db.SaveChangesAsync();
=======
            return _db.SaveChanges();
>>>>>>> ELI
        }
    }
}
