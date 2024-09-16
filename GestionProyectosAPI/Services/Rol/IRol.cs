using GestionProyectosAPI.DTOs;

namespace GestionProyectosAPI.Services.Rol
{
    public interface IRol
    {
        Task<int> PostRol(RolRequest rol);
        Task<List<RolResponse>> GetRols();
        Task<RolResponse> GetRol(int rolId);
        Task<int> PutRol(int rolId, RolRequest rol);
        Task<int> DeleteRol(int rolId);
    }
}
