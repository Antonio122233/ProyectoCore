using System;
using System.Collections.Generic;

namespace Domnio.Models;

public partial class TblCliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }

    public string? LugarDeContacto { get; set; }

    public string? Direccion { get; set; }

    public DateTime FechaRegistro { get; set; }

    public string? EstadoCliente { get; set; }

    public bool EstadoRegistro { get; set; }

    public virtual ICollection<TblVenta> TblVenta { get; set; } = new List<TblVenta>();
}
