using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectosAPI.Models;

public partial class Tareas
{
<<<<<<< HEAD:GestionProyectosAPI/Models/Tarea.cs
=======
    [Key]
    [Column("TareaId")]
>>>>>>> ELI:GestionProyectosAPI/Models/Tareas.cs
    public int TareaId { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
<<<<<<< HEAD:GestionProyectosAPI/Models/Tarea.cs
=======

    [Column("estadoTarea")]
    [StringLength(100)]
    [Unicode(false)]
>>>>>>> ELI:GestionProyectosAPI/Models/Tareas.cs
    public string EstadoTarea { get; set; } = null!;
    public DateOnly? FechaInicio { get; set; }
    public DateOnly? FechaFin { get; set; }
    public string? Prioridad { get; set; }
    public int MiembroEquipoId { get; set; }
    public int ProyectoId { get; set; }
<<<<<<< HEAD:GestionProyectosAPI/Models/Tarea.cs
    public virtual MiembroEquipo MiembroEquipo { get; set; } = null!;
=======

    [ForeignKey("MiembroEquipoId")]
    [InverseProperty("Tareas")]
    public virtual MiembroEquipos MiembroEquipo { get; set; } = null!;

    [ForeignKey("ProyectoId")]
    [InverseProperty("Tareas")]
>>>>>>> ELI:GestionProyectosAPI/Models/Tareas.cs
    public virtual Proyectos Proyecto { get; set; } = null!;
}
