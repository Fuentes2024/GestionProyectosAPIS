using GestionProyectosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionProyectosAPI.DTOs
{
    public class ProyectoResponse
    {
    
        public int ProyectoId { get; set; }
        public string Nombre { get; set; } = null!;    
        public string? Descripcion { get; set; }
        public DateOnly? FechaInicio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public decimal? Presupuesto { get; set; }
        public int UsuarioId { get; set; }
        public virtual UsuarioResponse Usuario { get; set; } = null!;
    }

    public class ProyectoRequest
    {

        public int ProyectoId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public DateOnly? FechaInicio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public decimal? Presupuesto { get; set; }
        public int UsuarioId { get; set; }
    }
}
