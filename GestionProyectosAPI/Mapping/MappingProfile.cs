using AutoMapper;
using GestionProyectosAPI.DTOs;
using GestionProyectosAPI.Models;

namespace GestionProyectosAPI.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            //Modelo -> DTO
            CreateMap<Proyectos, ProyectoResponse>();
            CreateMap<Usuarios, UsuarioResponse>();
            CreateMap<Rols, RolResponse>();
<<<<<<< HEAD
=======
            CreateMap<MiembroEquipos,MiembroEquipoResponse>();
            CreateMap<Tareas, TareaResponse>();
>>>>>>> ELI

            //DTO -> Modelo 
            CreateMap<ProyectoRequest, Proyectos>();
            CreateMap<UsuarioRequest, Usuarios>();
            CreateMap<RolRequest,  Rols>();
<<<<<<< HEAD
=======
            CreateMap<MiembroEquipoRequest, MiembroEquipos>();
            CreateMap<TareaRequest, Tareas>();
>>>>>>> ELI
        }
    }
}
