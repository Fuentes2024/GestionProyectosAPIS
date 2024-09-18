using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectosAPI.Models;

public partial class Informes
{
    [Key]
    [Column("informe_id")]
    public int InformeId { get; set; }

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

    [Column("estadoId")]
    public int EstadoId { get; set; }

    [Column("miembroEquipoId")]
    public int MiembroEquipoId { get; set; }

    [Column("usuarioId")]
    public int UsuarioId { get; set; }

    [Column("proyectoId")]
    public int ProyectoId { get; set; }

    [ForeignKey("EstadoId")]
    [InverseProperty("Informes")]
    public virtual Estados Estado { get; set; } = null!;

    [ForeignKey("MiembroEquipoId")]
    [InverseProperty("Informes")]
    public virtual MiembroEquipos MiembroEquipo { get; set; } = null!;

    [ForeignKey("ProyectoId")]
    [InverseProperty("Informes")]
    public virtual Proyectos Proyecto { get; set; } = null!;

    [ForeignKey("UsuarioId")]
    [InverseProperty("Informes")]
    public virtual Usuarios Usuario { get; set; } = null!;
}
