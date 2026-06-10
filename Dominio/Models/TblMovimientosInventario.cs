using System;
using System.Collections.Generic;

namespace Domnio.Models;

public partial class TblMovimientosInventario
{
    public int IdMovimiento { get; set; }

    public int IdProducto { get; set; }

    /// <summary>
    /// Tipo de Movimiento (Entrada &quot;E/ Salida &quot;S&quot;)
    /// </summary>
    public string TipoMovimiento { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public string? Observaciones { get; set; }

    public int Cantidad { get; set; }

    public int? IdReferencia { get; set; }

    public string? TablaReferencia { get; set; }

    public bool EstadoRegistro { get; set; }

    public virtual TblProducto IdProductoNavigation { get; set; } = null!;
}
