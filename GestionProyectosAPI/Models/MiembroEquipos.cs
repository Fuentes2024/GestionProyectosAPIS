using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectosAPI.Models;

public partial class MiembroEquipos
{
    [Key]
    [Column("miembroEquipoId")]
    public int MiembroEquipoId { get; set; }
    public string? Cargo { get; set; }

    [Column("usuarioId")]
    public int UsuarioId { get; set; }

    [InverseProperty("MiembroEquipos")]
    public virtual ICollection<Informes> Informe { get; set; } = new List<Informes>();

    [InverseProperty("MiembroEquipos")]
    public virtual ICollection<Tareas> Tarea { get; set; } = new List<Tareas>();

    [ForeignKey("UsuarioId")]
    [InverseProperty("MiembroEquipos")]
    public virtual Usuarios Usuario { get; set; } = null!;
}
