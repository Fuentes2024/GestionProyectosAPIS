using GestionProyectosAPI.Models;

namespace GestionProyectosAPI.DTOs
{
    public class TareaResponse
    {
      
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string EstadoTarea { get; set; } = null!;
        public DateOnly? FechaInicio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public string? Prioridad { get; set; }
        public int MiembroEquipoId { get; set; }
        public int ProyectoId { get; set; }
        public virtual MiembroEquipos MiembroEquipo { get; set; } = null!;
        public virtual Proyectos Proyecto { get; set; } = null!;
    }

    public class TareaRequest
    {
       
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string EstadoTarea { get; set; } = null!;
        public DateOnly? FechaInicio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public string? Prioridad { get; set; }
        public int MiembroEquipoId { get; set; }
        public int ProyectoId { get; set; }
        public virtual MiembroEquipos MiembroEquipo { get; set; } = null!;
        public virtual Proyectos Proyecto { get; set; } = null!;
    }
}
