using System;
using System.Collections.Generic;

namespace Dominio.Models;

public partial class TblCliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? Referencias { get; set; }

    public DateTime FechaRegistro { get; set; }

    /// <summary>
    /// 1 activo y 0 de baja
    /// </summary>
    public bool EstadoRegistro { get; set; }

    public virtual ICollection<TblVenta> TblVenta { get; set; } = new List<TblVenta>();
}
