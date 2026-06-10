using System;
using System.Collections.Generic;

namespace Domnio.Models;

public partial class TblCompra
{
    public int IdCompra { get; set; }

    public int? IdProveedor { get; set; }

    public string? Descripcion { get; set; }

    public decimal TotalCompra { get; set; }

    public int? IdTipoPago { get; set; }

    public DateOnly Fecha { get; set; }

    public bool EstadoRegistro { get; set; }

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    public virtual TblProveedore? IdProveedorNavigation { get; set; }

    public virtual TblTipoPago? IdTipoPagoNavigation { get; set; }
}
