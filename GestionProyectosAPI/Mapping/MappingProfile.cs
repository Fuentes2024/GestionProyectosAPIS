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

            //DTO -> Modelo 
            CreateMap<ProyectoRequest, Proyecto>();
            CreateMap<UsuarioRequest, Usuario>();
            CreateMap<RolRequest,  Proyecto>();
        }
    }
}
