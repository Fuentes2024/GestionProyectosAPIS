using AutoMapper;
using GestionProyectosAPI.DTOs;
using GestionProyectosAPI.Models;

namespace GestionProyectosAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Modelo -> DTO
            CreateMap<Proyectos, ProyectoResponse>();
            CreateMap<Usuarios, UsuarioResponse>();
            CreateMap<Rols, RolResponse>();
            CreateMap<MiembroEquipos, MiembroEquipoResponse>();
            CreateMap<Tareas, TareaResponse>();

            // DTO -> Modelo
            CreateMap<ProyectoRequest, Proyectos>();
            CreateMap<UsuarioRequest, Usuarios>();
            CreateMap<RolRequest, Rols>();
            CreateMap<MiembroEquipoRequest, MiembroEquipos>();
            CreateMap<TareaRequest, Tareas>();
        }
    }
}
