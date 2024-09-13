using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectosAPI.Models;

public partial class BbContext : DbContext
{
    public BbContext()
    {
    }

    public BbContext(DbContextOptions<BbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estado> Estado { get; set; }

    public virtual DbSet<Informe> Informe { get; set; }

    public virtual DbSet<MiembroEquipos> MiembroEquipos { get; set; }

    public virtual DbSet<Proyecto> Proyecto { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Tareas> Tareas { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("workstation id=ProyectoApi.mssql.somee.com;packet size=4096;user id=Ericklue65_SQLLogin_1;pwd=dkaofc33km;data source=ProyectoApi.mssql.somee.com;persist security info=False;initial catalog=ProyectoApi;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.EstadoId).HasName("PK__Estado__C696F30335BA4BC9");
        });

        modelBuilder.Entity<Informe>(entity =>
        {
            entity.HasKey(e => e.InformeId).HasName("PK__Informe__FB2E013262CB929C");

            entity.HasOne(d => d.Estado).WithMany(p => p.Informe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Informe__estadoI__48CFD27E");

            entity.HasOne(d => d.MiembroEquipo).WithMany(p => p.Informe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Informe__miembro__49C3F6B7");

            entity.HasOne(d => d.Proyecto).WithMany(p => p.Informe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Informe__proyect__4BAC3F29");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Informe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Informe__usuario__4AB81AF0");
        });

        modelBuilder.Entity<MiembroEquipos>(entity =>
        {
            entity.HasKey(e => e.MiembroEquipoId).HasName("PK__MiembroE__BD9FCD861705A428");

            entity.HasOne(d => d.Usuario).WithMany(p => p.MiembroEquipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MiembroEq__usuar__4222D4EF");
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.ProyectoId).HasName("PK__Proyecto__543423E6BEEFD5A9");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Proyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Proyecto__usuari__3F466844");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Rol__540236346EAD7207");
        });

        modelBuilder.Entity<Tareas>(entity =>
        {
            entity.HasKey(e => e.TareaId).HasName("PK__Tarea__E6731A803C9B86FF");

            entity.HasOne(d => d.MiembroEquipo).WithMany(p => p.Tarea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tarea__miembroEq__44FF419A");

            entity.HasOne(d => d.Proyecto).WithMany(p => p.Tarea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tarea__proyectoI__45F365D3");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__A5B1AB8E78509A88");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__rolId__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
