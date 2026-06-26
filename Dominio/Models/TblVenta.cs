using System;
using System.Collections.Generic;

namespace Dominio.Models;

public partial class TblVenta
{
    public int IdVenta { get; set; }

    public int IdCliente { get; set; }

    /// <summary>
    /// Credito o contado , dejalo asi como cadena, se va a enviar asi desde el sistema.
    /// </summary>
    public string? TipoVenta { get; set; }

    public decimal TotalVenta { get; set; }

    /// <summary>
    /// Sumatoria de los pagos, si es de contando se pone todo lo que pago, si es al credito poner los abonos
    /// </summary>
    public decimal MontoPagado { get; set; }

    public int? IdTipoPago { get; set; }

    public decimal? SaldoPendiente { get; set; }

    public DateTime FechaVenta { get; set; }

    /// <summary>
    /// si esta pendiente, pagado , etc
    /// </summary>
    public string EstadoPago { get; set; } = null!;

    public bool EstadoRegistro { get; set; }

    public virtual TblCliente IdClienteNavigation { get; set; } = null!;

    public virtual TblTipoPago? IdTipoPagoNavigation { get; set; }

    public virtual ICollection<TblAbono> TblAbonos { get; set; } = new List<TblAbono>();

    public virtual ICollection<TblDetalleVenta> TblDetalleVenta { get; set; } = new List<TblDetalleVenta>();
}
