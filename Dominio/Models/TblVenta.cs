using System;
using System.Collections.Generic;

namespace Domnio.Models;

public partial class TblVenta
{
    public int IdVenta { get; set; }

    public int IdCliente { get; set; }

    public DateTime Fecha { get; set; }

    public decimal TotalVenta { get; set; }

    /// <summary>
    /// NOT NULL DEFAULT 0
    /// </summary>
    public decimal MontoPagado { get; set; }

    public int? IdTipoPago { get; set; }

    public string? EstadoPago { get; set; }

    public decimal? SaldoPendiente { get; set; }

    /// <summary>
    /// Credito o contado , dejalo asi como cadena, se va a enviar asi desde el sistema.
    /// </summary>
    public string? TipoVenta { get; set; }

    public bool EstadoRegistro { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual TblEstadoPago? EstadoPagoNavigation { get; set; }

    public virtual TblCliente IdClienteNavigation { get; set; } = null!;

    public virtual TblTipoPago? IdTipoPagoNavigation { get; set; }

    public virtual ICollection<TblAbono> TblAbonos { get; set; } = new List<TblAbono>();
}
