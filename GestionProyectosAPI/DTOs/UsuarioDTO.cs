using GestionProyectosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionProyectosAPI.DTOs
{
    public class UsuarioResponse    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public int RolId { get; set; }
        public virtual RolResponse Rol { get; set; } = null!;
    }

    public class UsuarioRequest
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public int RolId { get; set; }
    }
}
