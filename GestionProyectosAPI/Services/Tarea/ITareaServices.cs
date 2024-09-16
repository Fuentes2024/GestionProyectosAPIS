using GestionProyectosAPI.DTOs;

namespace GestionProyectosAPI.Services.Tarea
{
    public interface ITareaServices
    {
        Task<int> PostTarea(TareaRequest tarea);
        Task<List<TareaResponse>> GetTareas();
        Task<TareaResponse> GetTarea(int tareaId);
        Task<int> PutTarea(int tareaId, TareaRequest tarea);
        Task<int> DeleteTarea(int tareaId);

    }
}
