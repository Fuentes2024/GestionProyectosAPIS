using AutoMapper;
using GestionProyectosAPI.DTOs;
using GestionProyectosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace GestionProyectosAPI.Services.Informe
{
    public class InformeServices : IInformeServices
    {
        private readonly BbContext _db;
        private readonly IMapper _mapper;
        public InformeServices(BbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<int> DeleteInforme(int informeId)
        {
            var inf = await _db.Informes.FindAsync(informeId);
            if (inf == null)
                return -1;
            _db.Informes.Remove(inf);

            return await _db.SaveChangesAsync();
        }
        public async Task<InformeResponse> GetInforme(int informeId)
        {
            var inf = await _db.Informes.FindAsync(informeId);
            var InformeResponse = _mapper.Map<Informes, InformeResponse>(inf);

            return InformeResponse;
        }

        public async Task<List<InformeResponse>> GetInformes()
        {
            var inf = await _db.Informes.ToListAsync();
            var InformeList = _mapper.Map<List<Informes>, List<InformeResponse>>(inf);

            return InformeList;
        }

        public async Task<int> PostInforme(InformeRequest informe)
        {
            var informeRequest = _mapper.Map<InformeRequest, Informes>(informe);
            await _db.Informes.AddAsync(informeRequest);

            return await _db.SaveChangesAsync();
        }

        public async Task<int> PutInforme(int informeId, InformeRequest informe)
        {
            var entity = await _db.Informes.FindAsync(informeId);
            if (entity == null)
                return -1;

            entity.Nombre = informe.Nombre;
            entity.Descripcion = informe.Descripcion;
            entity.FechaInicio = informe.FechaInicio;
            entity.FechaFin = informe.FechaFin;
            entity.EstadoId = informe.EstadoId;
            entity.MiembroEquipoId = informe.MiembroEquipoId;
            entity.UsuarioId = informe.UsuarioId;
            entity.ProyectoId = informe.ProyectoId;

            _db.Informes.Update(entity);

            return _db.SaveChanges();

        }
    }
}