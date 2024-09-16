using AutoMapper;
using GestionProyectosAPI.DTOs;
using GestionProyectosAPI.Models;

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
            var rol = await _db.Rol.FindAsync(rolId);
            if (rol == null)
                return -1;

          
           _db.Rol.Remove(rol);

            return await _db.SaveChangesAsync();
        }

        public Task<RolResponse> GetRol(int rolId)
        {
            throw new NotImplementedException();
        }

        public Task<List<RolResponse>> GetRols()
        {
            throw new NotImplementedException();
        }

        public Task<int> PostRol(RolRequest rol)
        {
            throw new NotImplementedException();
        }

        public Task<int> PutRol(int rolId, RolRequest rol)
        {
            throw new NotImplementedException();
        }
    }
}
