using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectosAPI.Models;

public partial class MiembroEquipo
{
  
    public int MiembroEquipoId { get; set; }
    public string? Cargo { get; set; }

    public int UsuarioId { get; set; }
    public virtual ICollection<Informe> Informe { get; set; } = new List<Informe>();

    public virtual ICollection<Tarea> Tarea { get; set; } = new List<Tarea>();

    public virtual Usuarios Usuario { get; set; } = null!;
}
