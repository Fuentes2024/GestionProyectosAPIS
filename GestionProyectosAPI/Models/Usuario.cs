using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectosAPI.Models;

[Index("Correo", Name = "UQ__Usuario__2A586E0B759FFA12", IsUnique = true)]
public partial class Usuario
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
    public virtual ICollection<Informe> Informe { get; set; } = new List<Informe>();

    [InverseProperty("Usuario")]
    public virtual ICollection<MiembroEquipos> MiembroEquipo { get; set; } = new List<MiembroEquipos>();

    [InverseProperty("Usuario")]
    public virtual ICollection<Proyecto> Proyecto { get; set; } = new List<Proyecto>();

    [ForeignKey("RolId")]
    [InverseProperty("Usuario")]
    public virtual Rol Rol { get; set; } = null!;
}
