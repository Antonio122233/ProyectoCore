using System;
using System.Collections.Generic;

namespace Domnio.Models;

public partial class TblUnidad
{
    public string? Nombre { get; set; }

    public int IdUnidad { get; set; }

    /// <summary>
    /// Pieza, caja, frasco, es como el envase o contenedor donde viene
    /// </summary>
    public string? Descripcion { get; set; }

    public bool? EstadoRegistro { get; set; }

    public virtual ICollection<TblProducto> TblProductos { get; set; } = new List<TblProducto>();
}
