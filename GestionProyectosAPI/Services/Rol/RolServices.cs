using AutoMapper;
using GestionProyectosAPI.DTOs;
using GestionProyectosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace GestionProyectosAPI.Services.Rol
{
    public class RolServices : IRol
    {
        private readonly BbContext _db;
        private readonly IMapper _mapper;

        public RolServices(BbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> DeleteRol(int rolId)
        {
            var rol = await _db.Rols.FindAsync(rolId);
            if (rol == null)
                return -1;

          
           _db.Rols.Remove(rol);

            return await _db.SaveChangesAsync();
        }

        public async Task<RolResponse> GetRol(int rolId)
        {
            var rol = await _db.Rols.FindAsync(rolId);
            var rolReponse = _mapper.Map<Rols, RolResponse>(rol);

            return rolReponse;
        }

        public async Task<List<RolResponse>> GetRols()
        {
            var rol = await _db.Rols.ToListAsync();
            var rolList = _mapper.Map<List<Rols>, List<RolResponse>>(rol);

            return rolList;
        }

        public async Task<int> PostRol(RolRequest rol)
        {
            var rolRequest = _mapper.Map<RolRequest, Rols>(rol);
            await _db.Rols.AddAsync(rolRequest);

            return await _db.SaveChangesAsync();
        }

        public async Task<int> PutRol(int rolId, RolRequest rol)
        {
            var entity = await _db.Rols.FindAsync(rolId);
            if (entity == null)
                return -1;

            entity.Nombre = rol.Nombre;

            _db.Rols.Update(entity);
            return _db.SaveChanges();
        }
    }
}
