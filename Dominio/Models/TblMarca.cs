using System;
using System.Collections.Generic;

namespace Dominio.Models;

public partial class TblMarca
{
    public int IdMarca { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public bool EstadoRegistro { get; set; }

    public virtual ICollection<TblProducto> TblProductos { get; set; } = new List<TblProducto>();
}
