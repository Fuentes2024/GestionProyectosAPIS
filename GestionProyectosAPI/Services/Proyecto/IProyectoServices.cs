using GestionProyectosAPI.DTOs;

namespace GestionProyectosAPI.Services.Proyecto
{
    public interface IProyectoServices
    {
<<<<<<< HEAD
        Task<int> PostProyecto(ProyectoRequest proyecto);
        Task<List<ProyectoResponse>>GetProyectos();
        Task<ProyectoResponse> GetProyecto(int proyectoId);
        Task<int> PutProyecto(int proyectoId, ProyectoRequest proyecto);
        Task<int> DeleteProyecto(int proyectoId);
=======

        Task<int> PostProyecto(ProyectoRequest proyecto);
        Task<List<ProyectoResponse>> GetProyectos();
        Task<ProyectoResponse> GetProyecto(int proyectoId);
        Task<int> putProyecto(int proyectoId, ProyectoRequest proyecto);
        Task<int> DeleteProyecto(int ProyectoId);
>>>>>>> ELI
    }
}
