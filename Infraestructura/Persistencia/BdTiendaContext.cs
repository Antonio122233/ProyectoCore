using System;
using System.Collections.Generic;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Persistencia;

public partial class BdTiendaContext : DbContext
{
    public BdTiendaContext()
    {
    }

    public BdTiendaContext(DbContextOptions<BdTiendaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAbono> TblAbonos { get; set; }

    public virtual DbSet<TblCategoria> TblCategorias { get; set; }

    public virtual DbSet<TblCliente> TblClientes { get; set; }

    public virtual DbSet<TblCompra> TblCompras { get; set; }

    public virtual DbSet<TblDetalleCompra> TblDetalleCompras { get; set; }

    public virtual DbSet<TblDetalleVenta> TblDetalleVentas { get; set; }

    public virtual DbSet<TblMarca> TblMarcas { get; set; }

    public virtual DbSet<TblMovimientosInventario> TblMovimientosInventarios { get; set; }

    public virtual DbSet<TblProducto> TblProductos { get; set; }

    public virtual DbSet<TblProveedore> TblProveedores { get; set; }

    public virtual DbSet<TblTipoPago> TblTipoPagos { get; set; }

    public virtual DbSet<TblVenta> TblVentas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=BdTienda;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAbono>(entity =>
        {
            entity.HasKey(e => e.IdAbono).HasName("PK_Abonos");

            entity.ToTable("tblAbonos");

            entity.Property(e => e.EstadoRegistro).HasDefaultValue(true);
            entity.Property(e => e.FechaAbono).HasColumnType("datetime");
            entity.Property(e => e.MontoAbono)
                .HasComment("NOT NULL CHECK (monto_abono > 0)")
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.TblAbonos)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblAbonos_tblVentas");
        });

        modelBuilder.Entity<TblCategoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK_tblCategoria");

            entity.ToTable("tblCategorias");

            entity.Property(e => e.Comentario)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("que si es ropa , joyeria , etc");
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
            entity.Property(e => e.EstadoRegistro)
                .HasDefaultValue(true)
                .HasComment("1 activo y 0 de baja");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Referencias)
                .HasMaxLength(500)
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
            entity.Property(e => e.EstadoRegistro).HasDefaultValue(true);
            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.IdTipoPago).HasComment("id del de tipo efectivo, credito");
            entity.Property(e => e.NumFactura)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Observaciones)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.TotalCompra).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.TblCompras)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK_Compras_Proveedores");

            entity.HasOne(d => d.IdTipoPagoNavigation).WithMany(p => p.TblCompras)
                .HasForeignKey(d => d.IdTipoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Compras_TipoPago");
        });

        modelBuilder.Entity<TblDetalleCompra>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCompra).HasName("PK_tblDetalleCompra");

            entity.ToTable("tblDetalleCompras");

            entity.Property(e => e.EstadoRegistro).HasDefaultValue(true);
            entity.Property(e => e.IdCompra).HasColumnName("idCompra");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.PrecioCompra)
                .HasComment("precio unitario del articulo")
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("precioCompra");
            entity.Property(e => e.Subtotal)
                .HasComputedColumnSql("([Cantidad]*[precioCompra])", true)
                .HasComment("Cantidad * precioCompra")
                .HasColumnType("decimal(29, 2)");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.TblDetalleCompras)
                .HasForeignKey(d => d.IdCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleCompra_Compras");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.TblDetalleCompras)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleCompra_Productos");
        });

        modelBuilder.Entity<TblDetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK_Detalle_Ventas");

            entity.ToTable("tblDetalleVentas");

            entity.Property(e => e.EstadoRegistro).HasDefaultValue(true);
            entity.Property(e => e.PrecioVenta)
                .HasComment("")
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SubTotal)
                .HasComputedColumnSql("([Cantidad]*[PrecioVenta])", true)
                .HasColumnType("decimal(29, 2)");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.TblDetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleVentas_Productos");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.TblDetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleVentas_Ventas");
        });

        modelBuilder.Entity<TblMarca>(entity =>
        {
            entity.HasKey(e => e.IdMarca);

            entity.ToTable("tblMarcas");

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

            entity.Property(e => e.CodigoProducto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Costo)
                .HasComment("Cuanto se tuvo que pagar por el producto")
                .HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EstadoRegistro).HasDefaultValue(true);
            entity.Property(e => e.ExistenciaActual).HasComment("cantidad actual del producto");
            entity.Property(e => e.ExistenciaMinima).HasComment("Existencia minima del producto, cuando deberia re abastecerse");
            entity.Property(e => e.FechaBaja).HasColumnType("datetime");
            entity.Property(e => e.Material)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Precio)
                .HasComment("Precio de venta al cliente")
                .HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Talla)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.TblProductos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_Productos_Categoria");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.TblProductos)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Marca");
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

        modelBuilder.Entity<TblVenta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK_Ventas");

            entity.ToTable("tblVentas");

            entity.Property(e => e.EstadoPago)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("si esta pendiente, pagado , etc");
            entity.Property(e => e.EstadoRegistro).HasDefaultValue(true);
            entity.Property(e => e.FechaVenta)
                .HasComment("")
                .HasColumnType("datetime");
            entity.Property(e => e.MontoPagado)
                .HasComment("Sumatoria de los pagos, si es de contando se pone todo lo que pago, si es al credito poner los abonos")
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SaldoPendiente)
                .HasComputedColumnSql("([TotalVenta]-[MontoPagado])", true)
                .HasColumnType("decimal(19, 2)");
            entity.Property(e => e.TipoVenta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Credito o contado , dejalo asi como cadena, se va a enviar asi desde el sistema.");
            entity.Property(e => e.TotalVenta).HasColumnType("decimal(18, 2)");

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
