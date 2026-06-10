using System;
using System.Collections.Generic;

namespace Domnio.Models;

public partial class DetalleVenta
{
    public int IdDetalleVenta { get; set; }

    public int IdVenta { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioVenta { get; set; }

    /// <summary>
    /// cantidad * precio_venta
    /// </summary>
    public decimal? Subtotal { get; set; }

    public bool EstadoRegistro { get; set; }

    public virtual TblProducto IdProductoNavigation { get; set; } = null!;

    public virtual TblVenta IdVentaNavigation { get; set; } = null!;
}
