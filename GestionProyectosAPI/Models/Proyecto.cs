using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectosAPI.Models;

public partial class Proyecto
{
    [Key]
    [Column("proyectoId")]
    public int ProyectoId { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [Column("fechaInicio")]
    public DateOnly? FechaInicio { get; set; }

    [Column("fechaFin")]
    public DateOnly? FechaFin { get; set; }

    [Column("presupuesto", TypeName = "decimal(18, 2)")]
    public decimal? Presupuesto { get; set; }

    [Column("usuarioId")]
    public int UsuarioId { get; set; }

    [InverseProperty("Proyecto")]
    public virtual ICollection<Informe> Informe { get; set; } = new List<Informe>();

    [InverseProperty("Proyecto")]
    public virtual ICollection<Tareas> Tarea { get; set; } = new List<Tareas>();

    [ForeignKey("UsuarioId")]
    [InverseProperty("Proyecto")]
    public virtual Usuario Usuario { get; set; } = null!;
}
