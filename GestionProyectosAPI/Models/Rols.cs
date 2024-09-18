using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public partial class Rols
{
    [Key]
    [Column("rolId")]
    public int RolId { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("Rol")]
    public virtual ICollection<Usuarios> Usuarios { get; set; } = new List<Usuarios>();
}
