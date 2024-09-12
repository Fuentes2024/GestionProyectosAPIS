using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectosAPI.Models;

public partial class Rol
{
    [Key]
    [Column("rolId")]
    public int RolId { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("Rol")]
    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
