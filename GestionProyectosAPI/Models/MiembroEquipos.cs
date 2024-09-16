using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectosAPI.Models;

public partial class MiembroEquipos
{
  
    public int MiembroEquipoId { get; set; }
    public string? Cargo { get; set; }

    public int UsuarioId { get; set; }
<<<<<<< HEAD:GestionProyectosAPI/Models/MiembroEquipo.cs
    public virtual ICollection<Informe> Informe { get; set; } = new List<Informe>();

    public virtual ICollection<Tarea> Tarea { get; set; } = new List<Tarea>();

=======

    [InverseProperty("MiembroEquipos")]
    public virtual ICollection<Informes> Informe { get; set; } = new List<Informes>();

    [InverseProperty("MiembroEquipos")]
    public virtual ICollection<Tareas> Tarea { get; set; } = new List<Tareas>();

    [ForeignKey("UsuarioId")]
    [InverseProperty("MiembroEquipos")]
>>>>>>> ELI:GestionProyectosAPI/Models/MiembroEquipos.cs
    public virtual Usuarios Usuario { get; set; } = null!;
}
