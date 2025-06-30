using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SistemaReclamo.Models;

public partial class DbtempMuñozContext : DbContext
{
    public DbtempMuñozContext()
    {
    }

    public DbtempMuñozContext(DbContextOptions<DbtempMuñozContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CConsumidor> CConsumidors { get; set; }

    public virtual DbSet<CEmpleado> CEmpleados { get; set; }

    public virtual DbSet<CEstado> CEstados { get; set; }

    public virtual DbSet<TAsesorium> TAsesoria { get; set; }

    public virtual DbSet<TAviso> TAvisos { get; set; }

    public virtual DbSet<TReclamo> TReclamos { get; set; }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CConsumidor>(entity =>
        {
            entity.HasKey(e => e.IdConsumidor).HasName("PK__c_Consum__4F69B1F6BA0BB4B3");

            entity.ToTable("c_Consumidor");

            entity.HasIndex(e => e.DuiConsumidor, "UQ__c_Consum__D8164B0F9DCA4CD2").IsUnique();

            entity.Property(e => e.IdConsumidor).HasColumnName("idConsumidor");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.ApeliidoConsumidor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apeliidoConsumidor");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correoElectronico");
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.DuiConsumidor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("duiConsumidor");
            entity.Property(e => e.NombreConsumidor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreConsumidor");
        });

        modelBuilder.Entity<CEmpleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__c_Emplea__5295297CB0B9F3E2");

            entity.ToTable("c_Empleado");

            entity.HasIndex(e => e.Usuario, "UQ__c_Emplea__9AFF8FC6B5AEF431").IsUnique();

            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.Clave)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombres");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<CEstado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__c_Estado__62EA894AF6D451C0");

            entity.ToTable("c_Estado");

            entity.Property(e => e.IdEstado).HasColumnName("idEstado");
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreEstado");
        });

        modelBuilder.Entity<TAsesorium>(entity =>
        {
            entity.HasKey(e => e.IdAsesoria).HasName("PK__t_Asesor__0C46A99948B4D529");

            entity.ToTable("t_Asesoria");

            entity.Property(e => e.IdAsesoria).HasColumnName("idAsesoria");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("datetime")
                .HasColumnName("fechaIngreso");
            entity.Property(e => e.IdReclamo).HasColumnName("idReclamo");
            entity.Property(e => e.MotivoAsesoria)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("motivoAsesoria");
            entity.Property(e => e.RespuestaAsesoria)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("respuestaAsesoria");

            entity.HasOne(d => d.IdReclamoNavigation).WithMany(p => p.TAsesoria)
                .HasForeignKey(d => d.IdReclamo)
                .HasConstraintName("FK__t_Asesori__idRec__47DBAE45");
        });

        modelBuilder.Entity<TAviso>(entity =>
        {
            entity.HasKey(e => e.IdAviso).HasName("PK__t_Aviso__D2A09AEF3D6CAF84");

            entity.ToTable("t_Aviso");

            entity.Property(e => e.IdAviso).HasColumnName("idAviso");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.DetalleAviso)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("detalleAviso");
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("datetime")
                .HasColumnName("fechaIngreso");
            entity.Property(e => e.IdReclamo).HasColumnName("idReclamo");

            entity.HasOne(d => d.IdReclamoNavigation).WithMany(p => p.TAvisos)
                .HasForeignKey(d => d.IdReclamo)
                .HasConstraintName("FK__t_Aviso__idRecla__48CFD27E");
        });

        modelBuilder.Entity<TReclamo>(entity =>
        {
            entity.HasKey(e => e.IdReclamo).HasName("PK__t_Reclam__5EB0D8645619EDEB");

            entity.ToTable("t_Reclamo");

            entity.Property(e => e.IdReclamo).HasColumnName("idReclamo");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.DetalleReclamo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("detalleReclamo");
            entity.Property(e => e.DireccionProveedor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("direccionProveedor");
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("datetime")
                .HasColumnName("fechaIngreso");
            entity.Property(e => e.FechaRevision)
                .HasColumnType("datetime")
                .HasColumnName("fechaRevision");
            entity.Property(e => e.IdConsumidor).HasColumnName("idConsumidor");
            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.IdEstado).HasColumnName("idEstado");
            entity.Property(e => e.MontoReclamo)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("montoReclamo");
            entity.Property(e => e.NombreProveedor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreProveedor");
            entity.Property(e => e.TelefonoProveedor)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("telefonoProveedor");

            entity.HasOne(d => d.IdConsumidorNavigation).WithMany(p => p.TReclamos)
                .HasForeignKey(d => d.IdConsumidor)
                .HasConstraintName("FK__t_Reclamo__idCon__45F365D3");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.TReclamos)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK__t_Reclamo__idEmp__44FF419A");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.TReclamos)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK__t_Reclamo__idEst__46E78A0C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
