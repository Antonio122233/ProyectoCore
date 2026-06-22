using System;
using System.Collections.Generic;

namespace Domnio.Models;

public partial class TblTipoPago
{
    public int IdTipoPago { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public bool EstadoRegistro { get; set; }

    public virtual ICollection<TblAbono> TblAbonos { get; set; } = new List<TblAbono>();

    public virtual ICollection<TblCompra> TblCompras { get; set; } = new List<TblCompra>();

    public virtual ICollection<TblVenta> TblVenta { get; set; } = new List<TblVenta>();
}
