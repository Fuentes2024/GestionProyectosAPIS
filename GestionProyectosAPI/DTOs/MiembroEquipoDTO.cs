using GestionProyectosAPI.Models;

namespace GestionProyectosAPI.DTOs
{
    public class MiembroEquipoResponse
    {
        public int MiembroEquipoId { get; set; }
        public string? Cargo { get; set; }

        public int UsuarioId { get; set; }

        public virtual Usuarios Usuario { get; set; } = null!;
    }

    public class MiembroEquipoRequest
    {
        public int MiembroEquipoId { get; set; }
        public string? Cargo { get; set; }

        public int UsuarioId { get; set; }

        public virtual Usuarios Usuario { get; set; } = null!;
    }
}
