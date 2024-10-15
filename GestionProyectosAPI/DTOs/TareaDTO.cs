using GestionProyectosAPI.Models;

namespace GestionProyectosAPI.DTOs
{
    public class TareaResponse
    {
        public int TareaId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string EstadoTarea { get; set; } = null!;
        public DateOnly? FechaInicio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public string? Prioridad { get; set; }
        public int MiembroEquipoId { get; set; }
        public int ProyectoId { get; set; }
    }

    public class PaginacionResponse<T>
    {
        public List<T> Items { get; set; }
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalElementos { get; set; }
        public bool TienePaginaAnterior => PaginaActual > 1;
        public bool TienePaginaSiguiente => PaginaActual < TotalPaginas;
    }


    public class TareaRequest
    {
        public int TareaId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string EstadoTarea { get; set; } = null!;
        public DateOnly? FechaInicio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public string? Prioridad { get; set; }
        public int MiembroEquipoId { get; set; }
        public int ProyectoId { get; set; }
    }

    public class PaginacionRequest
    {
        public int NumeroPagina { get; set; } = 1;
    }


}
