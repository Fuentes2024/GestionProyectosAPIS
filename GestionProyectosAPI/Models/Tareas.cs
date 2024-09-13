using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectosAPI.Models;

public partial class Tareas
{
    [Key]
    [Column("TareaId")]
    public int TareaId { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [Column("estadoTarea")]
    [StringLength(100)]
    [Unicode(false)]
    public string EstadoTarea { get; set; } = null!;

    [Column("fechaInicio")]
    public DateOnly? FechaInicio { get; set; }

    [Column("fechaFin")]
    public DateOnly? FechaFin { get; set; }

    [Column("prioridad")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Prioridad { get; set; }

    [Column("miembroEquipoId")]
    public int MiembroEquipoId { get; set; }

    [Column("proyectoId")]
    public int ProyectoId { get; set; }

    [ForeignKey("MiembroEquipoId")]
    [InverseProperty("Tareas")]
    public virtual MiembroEquipos MiembroEquipo { get; set; } = null!;

    [ForeignKey("ProyectoId")]
    [InverseProperty("Tareas")]
    public virtual Proyecto Proyecto { get; set; } = null!;
}
