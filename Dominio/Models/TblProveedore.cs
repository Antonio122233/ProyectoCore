using System;
using System.Collections.Generic;

namespace Dominio.Models;

public partial class TblProveedore
{
    public int IdProveedor { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public bool EstadoRegistro { get; set; }

    public virtual ICollection<TblCompra> TblCompras { get; set; } = new List<TblCompra>();
}
