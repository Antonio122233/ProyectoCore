using System;
using System.Collections.Generic;

namespace Dominio.Models;

public partial class TblCompra
{
    public int IdCompra { get; set; }

    public int? IdProveedor { get; set; }

    public string? NumFactura { get; set; }

    public decimal TotalCompra { get; set; }

    /// <summary>
    /// id del de tipo efectivo, credito
    /// </summary>
    public int IdTipoPago { get; set; }

    public string? Observaciones { get; set; }

    public DateOnly FechaCompra { get; set; }

    public bool EstadoRegistro { get; set; }

    public virtual TblProveedore? IdProveedorNavigation { get; set; }

    public virtual TblTipoPago IdTipoPagoNavigation { get; set; } = null!;

    public virtual ICollection<TblDetalleCompra> TblDetalleCompras { get; set; } = new List<TblDetalleCompra>();
}
