using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GestionProyectosAPI.Models;
using Microsoft.EntityFrameworkCore;

[Index("Correo", Name = "UQ__Usuario__2A586E0B759FFA12", IsUnique = true)]
public partial class Usuarios
{
    [Key]
    [Column("usuarioId")]
    public int UsuarioId { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("correo")]
    [StringLength(100)]
    [Unicode(false)]
    public string Correo { get; set; } = null!;

    [Column("clave")]
    [StringLength(100)]
    [Unicode(false)]
    public string Clave { get; set; } = null!;

    [Column("rolId")]
    public int RolId { get; set; }

    [InverseProperty("Usuario")]
    public virtual ICollection<Informes> Informes { get; set; } = new List<Informes>();

    [InverseProperty("Usuario")]
    public virtual ICollection<MiembroEquipos> MiembroEquipos { get; set; } = new List<MiembroEquipos>();

    [InverseProperty("Usuario")]
    public virtual ICollection<Proyectos> Proyectos { get; set; } = new List<Proyectos>();

    [ForeignKey("RolId")]
    [InverseProperty("Usuarios")]
    public virtual Rols Rol { get; set; } = null!;
}
