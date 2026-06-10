using System;
using System.Collections.Generic;

namespace Domnio.Models;

public partial class TblProducto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int? IdCategoria { get; set; }

    public int? StockActual { get; set; }

    public int? IdUnidad { get; set; }

    public int? IdMarca { get; set; }

    public string? Presentacion { get; set; }

    public string? Color { get; set; }

    public string? Talla { get; set; }

    public string? Material { get; set; }

    public decimal? PrecioCompra { get; set; }

    public decimal? PrecioVenta { get; set; }

    /// <summary>
    /// Este campo es para indicar cuando se puede generar una alerta por stock minimo, es decir cuando haya que re abastecer
    /// </summary>
    public int? CantidadMinima { get; set; }

    public DateOnly FechaRegistro { get; set; }

    public string? CodigoAsociadoDelProducto { get; set; }

    public bool EstadoRegistro { get; set; }

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual TblCategorium? IdCategoriaNavigation { get; set; }

    public virtual TblMarca? IdMarcaNavigation { get; set; }

    public virtual TblUnidad? IdUnidadNavigation { get; set; }

    public virtual ICollection<TblMovimientosInventario> TblMovimientosInventarios { get; set; } = new List<TblMovimientosInventario>();
}
