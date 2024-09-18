using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectosAPI.Models;

public partial class Proyectos
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
    public DateTime? FechaInicio { get; set; }

    [Column("fechaFin")]
    public DateTime? FechaFin { get; set; }

    [Column("presupuesto", TypeName = "decimal(18, 2)")]
    public decimal? Presupuesto { get; set; }

    [Column("usuarioId")]
    public int UsuarioId { get; set; }

    [InverseProperty("Proyecto")]
    public virtual ICollection<Informes> Informes { get; set; } = new List<Informes>();

    [InverseProperty("Proyecto")]
    public virtual ICollection<Tareas> Tareas { get; set; } = new List<Tareas>();

    [ForeignKey("UsuarioId")]
    [InverseProperty("Proyectos")]
    public virtual Usuarios Usuario { get; set; } = null!;
}
