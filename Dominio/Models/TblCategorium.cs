using System;
using System.Collections.Generic;

namespace Domnio.Models;

public partial class TblCategorium
{
    public int IdCategoria { get; set; }

    public string? Descripcion { get; set; }

    public string? Comentario { get; set; }

    public bool EstadoRegistro { get; set; }

    public virtual ICollection<TblProducto> TblProductos { get; set; } = new List<TblProducto>();
}
