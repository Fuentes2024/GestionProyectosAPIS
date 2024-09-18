using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectosAPI.Models
{
    public partial class MiembroEquipos
    {
        [Key]
        [Column("miembroEquipoId")]
        public int MiembroEquipoId { get; set; }

        [Column("cargo")]
        public string? Cargo { get; set; }

        [Column("usuarioId")]
        public int UsuarioId { get; set; }

        // Relación uno a muchos con Tareas
        public virtual ICollection<Tareas> Tareas { get; set; } = new HashSet<Tareas>();

        // Relación uno a muchos con Informes
        [InverseProperty("MiembroEquipo")]
        public virtual ICollection<Informes> Informes { get; set; } = new HashSet<Informes>();

        [ForeignKey("UsuarioId")]
        [InverseProperty("MiembroEquipos")]
        public virtual Usuarios Usuario { get; set; } = null!;
    }
}
