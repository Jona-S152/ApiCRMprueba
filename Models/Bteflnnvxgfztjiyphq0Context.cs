using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NPOI.OpenXmlFormats.Wordprocessing;

namespace ApiCRMprueba.Models;

public partial class Bteflnnvxgfztjiyphq0Context : DbContext
{

    private readonly IConfiguration _configuration;

    public Bteflnnvxgfztjiyphq0Context()
    {
    }

    public Bteflnnvxgfztjiyphq0Context(DbContextOptions<Bteflnnvxgfztjiyphq0Context> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolOpcione> RolOpciones { get; set; }

    public virtual DbSet<RolRolOpcione> RolRolOpciones { get; set; }

    public virtual DbSet<RolUsuario> RolUsuarios { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PRIMARY");

            entity.ToTable("Persona");

            entity.Property(e => e.Apellidos).HasMaxLength(60);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Identificacion).HasMaxLength(10);
            entity.Property(e => e.Nombre).HasMaxLength(60);
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PRIMARY");

            entity.ToTable("Rol");

            entity.Property(e => e.RolName).HasMaxLength(50);
        });

        modelBuilder.Entity<RolOpcione>(entity =>
        {
            entity.HasKey(e => e.IdOpcion).HasName("PRIMARY");

            entity.Property(e => e.NombreOpcion).HasMaxLength(50);
        });

        modelBuilder.Entity<RolRolOpcione>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Rol_RolOpciones");

            entity.HasIndex(e => e.RolOpcionesIdOpciones, "RolOpciones_IdOpciones");

            entity.HasIndex(e => e.RolIdRol, "Rol_IdRol");

            entity.Property(e => e.RolIdRol).HasColumnName("Rol_IdRol");
            entity.Property(e => e.RolOpcionesIdOpciones).HasColumnName("RolOpciones_IdOpciones");

            entity.HasOne(d => d.RolIdRolNavigation).WithMany()
                .HasForeignKey(d => d.RolIdRol)
                .HasConstraintName("Rol_RolOpciones_ibfk_1");

            entity.HasOne(d => d.RolOpcionesIdOpcionesNavigation).WithMany()
                .HasForeignKey(d => d.RolOpcionesIdOpciones)
                .HasConstraintName("Rol_RolOpciones_ibfk_2");
        });

        modelBuilder.Entity<RolUsuario>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Rol_Usuarios");

            entity.HasIndex(e => e.RolIdRol, "Rol_IdRol");

            entity.HasIndex(e => e.UsuariosIdUsuario, "Usuarios_IdUsuario");

            entity.Property(e => e.RolIdRol).HasColumnName("Rol_IdRol");
            entity.Property(e => e.UsuariosIdUsuario).HasColumnName("Usuarios_IdUsuario");

            entity.HasOne(d => d.RolIdRolNavigation).WithMany()
                .HasForeignKey(d => d.RolIdRol)
                .HasConstraintName("Rol_Usuarios_ibfk_1");

            entity.HasOne(d => d.UsuariosIdUsuarioNavigation).WithMany()
                .HasForeignKey(d => d.UsuariosIdUsuario)
                .HasConstraintName("Rol_Usuarios_ibfk_2");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasNoKey();

            entity.HasIndex(e => e.UsuariosIdUsuario, "usuarios_idUsuario");

            entity.Property(e => e.FechaCierre).HasColumnType("date");
            entity.Property(e => e.FechaIngreso).HasColumnType("date");
            entity.Property(e => e.UsuariosIdUsuario).HasColumnName("usuarios_idUsuario");

            entity.HasOne(d => d.UsuariosIdUsuarioNavigation).WithMany()
                .HasForeignKey(d => d.UsuariosIdUsuario)
                .HasConstraintName("Sessions_ibfk_1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.HasIndex(e => e.PersonaIdPersona, "Persona_IdPersona");

            entity.Property(e => e.Mail).HasMaxLength(120);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PersonaIdPersona).HasColumnName("Persona_IdPersona");
            entity.Property(e => e.SessionActive)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.oPersona).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.PersonaIdPersona)
                .HasConstraintName("Usuarios_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
