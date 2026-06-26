using System;
using System.Collections.Generic;

namespace Dominio.Models;

public partial class TblDetalleCompra
{
    public int IdDetalleCompra { get; set; }

    public int IdCompra { get; set; }

    public int IdProducto { get; set; }

    /// <summary>
    /// precio unitario del articulo
    /// </summary>
    public decimal PrecioCompra { get; set; }

    public int Cantidad { get; set; }

    /// <summary>
    /// Cantidad * precioCompra
    /// </summary>
    public decimal? Subtotal { get; set; }

    public bool EstadoRegistro { get; set; }

    public virtual TblCompra IdCompraNavigation { get; set; } = null!;

    public virtual TblProducto IdProductoNavigation { get; set; } = null!;
}
