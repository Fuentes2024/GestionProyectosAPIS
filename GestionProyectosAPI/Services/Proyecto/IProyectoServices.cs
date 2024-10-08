﻿using GestionProyectosAPI.DTOs;

namespace GestionProyectosAPI.Services.Proyecto
{
    public interface IProyectoServices
    {
        Task<int> PostProyecto(ProyectoRequest proyecto);
        Task<List<ProyectoResponse>> GetProyectos();
        Task<ProyectoResponse> GetProyecto(int proyectoId);
        Task<int> PutProyecto(int proyectoId, ProyectoRequest proyecto);
        Task<int> DeleteProyecto(int proyectoId);
    }
}
