using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectosAPI.Models;

public partial class MiembroEquipo
{
    [Key]
    [Column("miembroEquipoId")]
    public int MiembroEquipoId { get; set; }

    [Column("cargo")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Cargo { get; set; }

    [Column("usuarioId")]
    public int UsuarioId { get; set; }

    [InverseProperty("MiembroEquipo")]
    public virtual ICollection<Informe> Informe { get; set; } = new List<Informe>();

    [InverseProperty("MiembroEquipo")]
    public virtual ICollection<Tarea> Tarea { get; set; } = new List<Tarea>();

    [ForeignKey("UsuarioId")]
    [InverseProperty("MiembroEquipo")]
    public virtual Usuario Usuario { get; set; } = null!;
}
