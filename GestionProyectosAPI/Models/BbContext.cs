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

    public virtual DbSet<Estados> Estados { get; set; }
    public virtual DbSet<Informes> Informes { get; set; }
    public virtual DbSet<MiembroEquipos> MiembroEquipos { get; set; }
    public virtual DbSet<Proyectos> Proyectos { get; set; }
    public virtual DbSet<Rols> Rols { get; set; }
    public virtual DbSet<Tareas> Tareas { get; set; }
    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estados>(entity =>
        {
            entity.HasKey(e => e.EstadoId).HasName("PK__Estado__C696F30335BA4BC9");
        });

        modelBuilder.Entity<Informes>(entity =>
        {
            entity.HasKey(e => e.InformeId).HasName("PK__Informe__FB2E013262CB929C");

            entity.HasOne(d => d.Estado)
                .WithMany(p => p.Informes)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Informe__estadoI__48CFD27E");

            entity.HasOne(d => d.MiembroEquipo)
                .WithMany(p => p.Informes)
                .HasForeignKey(d => d.MiembroEquipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Informe__miembro__49C3F6B7");

            entity.HasOne(d => d.Proyecto)
                .WithMany(p => p.Informes)
                .HasForeignKey(d => d.ProyectoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Informe__proyect__4BAC3F29");

            entity.HasOne(d => d.Usuario)
                .WithMany(p => p.Informes)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Informe__usuario__4AB81AF0");
        });

        modelBuilder.Entity<MiembroEquipos>(entity =>
        {
            entity.HasKey(e => e.MiembroEquipoId).HasName("PK__MiembroE__BD9FCD861705A428");

            entity.HasOne(d => d.Usuario)
                .WithMany(p => p.MiembroEquipos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MiembroEq__usuar__4222D4EF");
        });

        modelBuilder.Entity<Proyectos>(entity =>
        {
            entity.HasKey(e => e.ProyectoId).HasName("PK__Proyecto__543423E6BEEFD5A9");

            entity.HasOne(d => d.Usuario)
                .WithMany(p => p.Proyectos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Proyecto__usuari__3F466844");
        });

        modelBuilder.Entity<Rols>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Rol__540236346EAD7207");
        });

        modelBuilder.Entity<Tareas>(entity =>
        {
            entity.HasKey(e => e.TareaId).HasName("PK__Tarea__E6731A803C9B86FF");

            entity.HasOne(d => d.MiembroEquipo)
                .WithMany(p => p.Tareas)
                .HasForeignKey(d => d.MiembroEquipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tarea__miembroEq__44FF419A");

            entity.HasOne(d => d.Proyecto)
                .WithMany(p => p.Tareas)
                .HasForeignKey(d => d.ProyectoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tarea__proyectoI__45F365D3");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__A5B1AB8E78509A88");

            entity.HasOne(d => d.Rol)
                .WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__rolId__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
