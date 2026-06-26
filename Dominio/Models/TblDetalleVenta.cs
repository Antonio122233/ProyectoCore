using System;
using System.Collections.Generic;

namespace Dominio.Models;

public partial class TblDetalleVenta
{
    public int IdDetalleVenta { get; set; }

    public int IdVenta { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioVenta { get; set; }

    public decimal? SubTotal { get; set; }

    public bool EstadoRegistro { get; set; }

    public virtual TblProducto IdProductoNavigation { get; set; } = null!;

    public virtual TblVenta IdVentaNavigation { get; set; } = null!;
}
