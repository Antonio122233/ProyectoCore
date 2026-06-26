using System;
using System.Collections.Generic;

namespace Dominio.Models;

public partial class TblCategoria
{
    public int IdCategoria { get; set; }

    /// <summary>
    /// que si es ropa , joyeria , etc
    /// </summary>
    public string? Descripcion { get; set; }

    public string? Comentario { get; set; }

    public bool EstadoRegistro { get; set; }

    public virtual ICollection<TblProducto> TblProductos { get; set; } = new List<TblProducto>();
}
