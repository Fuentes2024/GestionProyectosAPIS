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

    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }

    [Column("estadoTarea")]
    [StringLength(100)]
    [Unicode(false)]
    public string EstadoTarea { get; set; } = null!;

    public DateOnly? FechaInicio { get; set; }
    public DateOnly? FechaFin { get; set; }
    public string? Prioridad { get; set; }
    public int MiembroEquipoId { get; set; }
    public int ProyectoId { get; set; }

    [ForeignKey("MiembroEquipoId")]
    [InverseProperty("Tareas")]
    public virtual MiembroEquipos MiembroEquipo { get; set; } = null!;

    [ForeignKey("ProyectoId")]
    [InverseProperty("Tareas")]
    public virtual Proyectos Proyecto { get; set; } = null!;
}
