using System;
using System.Collections.Generic;
namespace Dominio.Models;

public partial class TblAbono
{
    public int IdAbono { get; set; }

    public int IdVenta { get; set; }

    public DateTime FechaAbono { get; set; }

    /// <summary>
    /// NOT NULL CHECK (monto_abono &gt; 0)
    /// </summary>
    public decimal MontoAbono { get; set; }

    public int IdTipoPago { get; set; }

    public string? Observaciones { get; set; }

    public bool EstadoRegistro { get; set; }

    public virtual TblVenta IdVentaNavigation { get; set; } = null!;
}
