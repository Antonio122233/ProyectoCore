using System;
using System.Collections.Generic;
using Domnio.Models;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura;

public partial class BdTiendaContext : DbContext
{
    public BdTiendaContext()
    {
    }

    public BdTiendaContext(DbContextOptions<BdTiendaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DetalleCompra> DetalleCompras { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }

    public virtual DbSet<TblAbono> TblAbonos { get; set; }

    public virtual DbSet<TblCategorium> TblCategoria { get; set; }

    public virtual DbSet<TblCliente> TblClientes { get; set; }

    public virtual DbSet<TblCompra> TblCompras { get; set; }

    public virtual DbSet<TblEstadoPago> TblEstadoPagos { get; set; }

    public virtual DbSet<TblMarca> TblMarcas { get; set; }

    public virtual DbSet<TblMovimientosInventario> TblMovimientosInventarios { get; set; }

    public virtual DbSet<TblProducto> TblProductos { get; set; }

    public virtual DbSet<TblProveedore> TblProveedores { get; set; }

    public virtual DbSet<TblTipoPago> TblTipoPagos { get; set; }

    public virtual DbSet<TblUnidad> TblUnidads { get; set; }

    public virtual DbSet<TblVenta> TblVentas { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost;Database=BdTienda;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DetalleCompra>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCompra).HasName("PK_tblDetalleCompra");

            entity.ToTable("Detalle_Compra");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.IdCompra).HasColumnName("idCompra");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.PrecioCompra)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("precioCompra");
            entity.Property(e => e.Subtotal)
                .HasComputedColumnSql("([Cantidad]*[precioCompra])", true)
                .HasComment("Cantidad * precioCompra")
                .HasColumnType("decimal(29, 2)");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.IdCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleCompra_Compras");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleCompra_Productos");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta);

            entity.ToTable("Detalle_Ventas");

            entity.Property(e => e.IdDetalleVenta).HasColumnName("idDetalleVENTA");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.EstadoRegistro).HasDefaultValue(true);
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.PrecioVenta)
                .HasComment("")
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("precio_venta");
            entity.Property(e => e.Subtotal)
                .HasComputedColumnSql("([cantidad]*[precio_venta])", true)
                .HasComment("cantidad * precio_venta")
                .HasColumnType("decimal(29, 2)")
                .HasColumnName("subtotal");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleVentas_Productos");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleVentas_Ventas");
        });

        modelBuilder.Entity<TblAbono>(entity =>
        {
            entity.HasKey(e => e.IdAbono).HasName("PK_Abonos");

            entity.ToTable("tblAbonos");

            entity.Property(e => e.IdAbono).HasColumnName("idAbono");
            entity.Property(e => e.EstadoRegistro).HasDefaultValue(true);
            entity.Property(e => e.FechaAbono)
                .HasColumnType("datetime")
                .HasColumnName("fecha_abono");
            entity.Property(e => e.IdFormaPago).HasColumnName("idFormaPago");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.MontoAbono)
                .HasComment("NOT NULL CHECK (monto_abono > 0)")
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("monto_abono");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("observaciones");

            entity.HasOne(d => d.IdFormaPagoNavigation).WithMany(p => p.TblAbonos)
                .HasForeignKey(d => d.IdFormaPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Abonos_TipoPago");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.TblAbonos)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Abonos_Ventas");
        });

        modelBuilder.Entity<TblCategorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria);

            entity.ToTable("tblCategoria");

            entity.Property(e => e.Comentario)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EstadoRegistro).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblCliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK_Clientes");

            entity.ToTable("tblClientes");

            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.EstadoCliente)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EstadoRegistro).HasDefaultValue(true);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LugarDeContacto)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblCompra>(entity =>
        {
            entity.HasKey(e => e.IdCompra);

            entity.ToTable("tblCompras");

            entity.Property(e => e.IdCompra).HasColumnName("idCompra");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EstadoRegistro).HasDefaultValue(true);
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.TotalCompra).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.TblCompras)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK_Compras_Proveedores");

            entity.HasOne(d => d.IdTipoPagoNavigation).WithMany(p => p.TblCompras)
                .HasForeignKey(d => d.IdTipoPago)
                .HasConstraintName("FK_Compras_TipoPago");
        });

        modelBuilder.Entity<TblEstadoPago>(entity =>
        {
            entity.HasKey(e => e.IdEstadoPago);

            entity.ToTable("tblEstadoPago");

            entity.Property(e => e.IdEstadoPago)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idEstadoPago");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblMarca>(entity =>
        {
            entity.ToTable("tblMarcas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EstadoRegistro).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblMovimientosInventario>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento);

            entity.ToTable("tblMovimientosInventario");

            entity.Property(e => e.EstadoRegistro).HasDefaultValue(true);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Observaciones).HasMaxLength(500);
            entity.Property(e => e.TablaReferencia)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoMovimiento)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("Tipo de Movimiento (Entrada \"E/ Salida \"S\")");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.TblMovimientosInventarios)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movimientos_Productos");
        });

        modelBuilder.Entity<TblProducto>(entity =>
        {
            entity.HasKey(e => e.IdProducto);

            entity.ToTable("tblProductos");

            entity.Property(e => e.CantidadMinima).HasComment("Este campo es para indicar cuando se puede generar una alerta por stock minimo, es decir cuando haya que re abastecer");
            entity.Property(e => e.CodigoAsociadoDelProducto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EstadoRegistro).HasDefaultValue(true);
            entity.Property(e => e.Material)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PrecioCompra).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Presentacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Talla)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.TblProductos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_Productos_Categoria");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.TblProductos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("FK_Productos_Marca");

            entity.HasOne(d => d.IdUnidadNavigation).WithMany(p => p.TblProductos)
                .HasForeignKey(d => d.IdUnidad)
                .HasConstraintName("FK_Productos_Unidad");
        });

        modelBuilder.Entity<TblProveedore>(entity =>
        {
            entity.HasKey(e => e.IdProveedor);

            entity.ToTable("tblProveedores");

            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EstadoRegistro).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblTipoPago>(entity =>
        {
            entity.HasKey(e => e.IdTipoPago);

            entity.ToTable("tblTipoPago");

            entity.Property(e => e.IdTipoPago).HasColumnName("idTipoPago");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EstadoRegistro).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblUnidad>(entity =>
        {
            entity.HasKey(e => e.IdUnidad);

            entity.ToTable("tblUnidad");

            entity.Property(e => e.IdUnidad).HasColumnName("idUnidad");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Pieza, caja, frasco, es como el envase o contenedor donde viene");
            entity.Property(e => e.EstadoRegistro).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<TblVenta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK_Ventas");

            entity.ToTable("tblVentas");

            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.EstadoPago)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EstadoRegistro).HasDefaultValue(true);
            entity.Property(e => e.Fecha)
                .HasComment("")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdTipoPago).HasColumnName("idTipoPago");
            entity.Property(e => e.MontoPagado)
                .HasComment("NOT NULL DEFAULT 0")
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("monto_pagado");
            entity.Property(e => e.SaldoPendiente)
                .HasComputedColumnSql("([total_venta]-[monto_pagado])", true)
                .HasColumnType("decimal(19, 2)")
                .HasColumnName("saldo_pendiente");
            entity.Property(e => e.TipoVenta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Credito o contado , dejalo asi como cadena, se va a enviar asi desde el sistema.");
            entity.Property(e => e.TotalVenta)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total_venta");

            entity.HasOne(d => d.EstadoPagoNavigation).WithMany(p => p.TblVenta)
                .HasForeignKey(d => d.EstadoPago)
                .HasConstraintName("FK_tblVentas_tblEstadoPago");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.TblVenta)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Clientes");

            entity.HasOne(d => d.IdTipoPagoNavigation).WithMany(p => p.TblVenta)
                .HasForeignKey(d => d.IdTipoPago)
                .HasConstraintName("FK_Ventas_TipoPago");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
