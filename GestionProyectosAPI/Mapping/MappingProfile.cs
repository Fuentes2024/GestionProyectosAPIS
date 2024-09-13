using AutoMapper;
using GestionProyectosAPI.DTOs;
using GestionProyectosAPI.Models;

namespace GestionProyectosAPI.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            //Modelo -> DTO
            CreateMap<Proyecto, ProyectoResponse>();
            CreateMap<Usuario, UsuarioResponse>();
            CreateMap<Rol, RolResponse>();
            CreateMap<MiembroEquipos,MiembroEquipoResponse>();
            CreateMap<Tareas, TareaResponse>();

            //DTO -> Modelo 
            CreateMap<ProyectoRequest, Proyecto>();
            CreateMap<UsuarioRequest, Usuario>();
            CreateMap<RolRequest,  Proyecto>();
            CreateMap<MiembroEquipoRequest, MiembroEquipos>();
            CreateMap<TareaRequest, Tareas>();
        }
    }
}
