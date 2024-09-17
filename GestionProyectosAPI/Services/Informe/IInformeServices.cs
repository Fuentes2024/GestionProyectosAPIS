using GestionProyectosAPI.DTOs;

namespace GestionProyectosAPI.Services.Informe
{
    public interface IInformeServices
    {
        Task<int> PostInforme(InformeRequest informe);
        Task<List<InformeResponse>> GetInformes();
        Task<InformeResponse> GetInforme(int informeId);
        Task<int> PutInforme(int informeId, InformeRequest informe);
        Task<int> DeleteInforme(int informeId);
    }
}

