using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectosAPI.Models;

public partial class Estado
{
    [Key]
    [Column("estadoId")]
    public int EstadoId { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("Estado")]
    public virtual ICollection<Informe> Informe { get; set; } = new List<Informe>();
}
