using System;
using System.Collections.Generic;

namespace Domnio.Models;

public partial class DetalleCompra
{
    public int IdDetalleCompra { get; set; }

    public int IdCompra { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioCompra { get; set; }

    public bool Activo { get; set; }

    /// <summary>
    /// Cantidad * precioCompra
    /// </summary>
    public decimal? Subtotal { get; set; }

    public virtual TblCompra IdCompraNavigation { get; set; } = null!;

    public virtual TblProducto IdProductoNavigation { get; set; } = null!;
}
