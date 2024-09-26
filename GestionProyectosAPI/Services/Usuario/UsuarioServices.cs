using AutoMapper;
using GestionProyectosAPI.DTOs;
using GestionProyectosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectosAPI.Services.Usuario
{
    public class UsuarioServices: IUsuarioServices
    {
        private readonly BbContext _db;
        private readonly IMapper _mapper;

        public UsuarioServices(BbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> DeleteUsuario(int usuarioId)
        {
            var usuario = await _db.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
                return -1;

            _db.Usuarios.Remove(usuario);

            return await _db.SaveChangesAsync();

        }

        public async Task<UsuarioResponse> GetUsuario(int usuarioId)
        {
            var usuario = await _db.Usuarios.FindAsync(usuarioId);
            var usuarioResponse = _mapper.Map<Usuarios, UsuarioResponse>(usuario);

            return usuarioResponse;
        }

        public async Task<List<UsuarioResponse>> GetUsuarios()
        {
            var usuario = await _db.Usuarios.ToListAsync();
            var usuarioList = _mapper.Map<List<Usuarios>, List<UsuarioResponse>>(usuario);

            return usuarioList;
        }

        public async Task<UsuarioResponse> Login(UsuarioRequest usuario)
        {
            var usuarioEntity = await _db.Usuarios.FirstOrDefaultAsync(
                            o => o.Correo == usuario.Correo
                            && o.Clave == usuario.Clave
                );
            var usuarioResponse = _mapper.Map<Usuarios, UsuarioResponse>(usuarioEntity);
            return usuarioResponse;

        }

        public async Task<int> PostUsuario(UsuarioRequest usuario)
        {
            var usuarioRequest = _mapper.Map<UsuarioRequest, Usuarios>(usuario);
            await _db.Usuarios.AddAsync(usuarioRequest);

            return await _db.SaveChangesAsync();
        }

        public async Task<int> PutUsuario(int usuarioId, UsuarioRequest usuario)
        {

            var entity = await _db.Usuarios.FindAsync(usuarioId);
            if (entity == null)
                return -1;

            entity.Nombre = usuario.Nombre;
            entity.Correo = usuario.Correo;
            entity.Clave = usuario.Clave;
            entity.RolId = usuario.RolId;

            _db.Usuarios.Update(entity);

            return await _db.SaveChangesAsync();
        }
    }
}
