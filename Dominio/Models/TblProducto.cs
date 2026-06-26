using System;
using System.Collections.Generic;

namespace Dominio.Models;

public partial class TblProducto
{
    public int IdProducto { get; set; }

    public int? IdCategoria { get; set; }

    public int IdMarca { get; set; }

    public string? CodigoProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Color { get; set; }

    public string? Talla { get; set; }

    public string? Material { get; set; }

    /// <summary>
    /// Cuanto se tuvo que pagar por el producto
    /// </summary>
    public decimal Costo { get; set; }

    /// <summary>
    /// Precio de venta al cliente
    /// </summary>
    public decimal Precio { get; set; }

    /// <summary>
    /// cantidad actual del producto
    /// </summary>
    public int ExistenciaActual { get; set; }

    /// <summary>
    /// Existencia minima del producto, cuando deberia re abastecerse
    /// </summary>
    public int ExistenciaMinima { get; set; }

    public DateOnly FechaRegistro { get; set; }

    public bool EstadoRegistro { get; set; }

    public DateTime FechaBaja { get; set; }

    public virtual TblCategoria? IdCategoriaNavigation { get; set; }

    public virtual TblMarca IdMarcaNavigation { get; set; } = null!;

    public virtual ICollection<TblDetalleCompra> TblDetalleCompras { get; set; } = new List<TblDetalleCompra>();

    public virtual ICollection<TblDetalleVenta> TblDetalleVenta { get; set; } = new List<TblDetalleVenta>();

    public virtual ICollection<TblMovimientosInventario> TblMovimientosInventarios { get; set; } = new List<TblMovimientosInventario>();
}
