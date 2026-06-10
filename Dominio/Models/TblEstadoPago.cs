using System;
using System.Collections.Generic;

namespace Domnio.Models;

public partial class TblEstadoPago
{
    public string IdEstadoPago { get; set; } = null!;

    public string? Nombre { get; set; }

    public bool EstadoRegistro { get; set; }

    public virtual ICollection<TblVenta> TblVenta { get; set; } = new List<TblVenta>();
}
