using AutoMapper;
using GestionProyectosAPI.DTOs;
using GestionProyectosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace GestionProyectosAPI.Services.Miembro
{
    public class MiembroServices : IMiembroServices
    {

        private readonly BbContext _db;
        private readonly IMapper _mapper;

        public MiembroServices(BbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<int> DeleteMiembro(int miembroEquipoId)
        {
            var miembro = await _db.MiembroEquipos.FindAsync(miembroEquipoId);
            if (miembro == null)
                return -1;

            _db.MiembroEquipos.Remove(miembro);

            return await _db.SaveChangesAsync();
        }

        public async Task<MiembroEquipoResponse> GetMiembro(int miembroEquipoId)
        {
            var miembro = await _db.MiembroEquipos.FindAsync(miembroEquipoId);
            var miembroreponse = _mapper.Map<MiembroEquipos, MiembroEquipoResponse>(miembro);

            return miembroreponse;
        }

        public async Task<List<MiembroEquipoResponse>> GetMiembros()
        {
            var miembroE = await _db.MiembroEquipos.ToListAsync();
            var miembroList = _mapper.Map<List<MiembroEquipos>, List<MiembroEquipoResponse>>(miembroE);

            return miembroList;
        }

        public async Task<int> PostMiembro(MiembroEquipoRequest miembroEquipo)
        {
            var miembrorequest = _mapper.Map<MiembroEquipoRequest, MiembroEquipos>(miembroEquipo);
            await _db.MiembroEquipos.AddAsync(miembrorequest);

            return await _db.SaveChangesAsync();
        }

        public async Task<int> PutMiembro(int miembroEquipoId, MiembroEquipoRequest miembroEquipo)
        {
            var entity = await _db.MiembroEquipos.FindAsync(miembroEquipoId);
            if (entity == null)
                return -1;

            entity.Cargo = miembroEquipo.Cargo;
            entity.UsuarioId = miembroEquipo.UsuarioId;
            //ok
            _db.MiembroEquipos.Update(entity);
            return _db.SaveChanges();
        }
    }
}
