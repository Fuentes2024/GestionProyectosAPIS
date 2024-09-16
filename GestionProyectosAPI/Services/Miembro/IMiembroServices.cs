using GestionProyectosAPI.DTOs;

namespace GestionProyectosAPI.Services.Miembro
{
    public interface IMiembroServices
    {
        Task<int> PostMiembro(MiembroEquipoRequest miembroEquipo);
        Task<List<MiembroEquipoResponse>> GetMiembros();
        Task<MiembroEquipoResponse> GetMiembro(int miembroEquipoId);
        Task<int> PutMiembro(int miembroEquipoId, MiembroEquipoRequest miembroEquipo);
        Task<int> DeleteMiembro(int miembroEquipoId);
    }
}
