using GestionProyectosAPI.Models;

namespace GestionProyectosAPI.DTOs
{
    public class MiembroEquipoResponse
    {
        public int MiembroEquipoId { get; set; }
        public string? Cargo { get; set; }

        public int UsuarioId { get; set; }


    }

    public class MiembroEquipoRequest
    {
        public int MiembroEquipoId { get; set; }
        public string? Cargo { get; set; }

        public int UsuarioId { get; set; }

    }
}
