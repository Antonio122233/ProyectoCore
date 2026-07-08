using Aplicacion.DTOs.TipoPago;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Repositorios
{
    public interface IProductoRepository : IGenericRepository<TblProducto>
    {
        Task<TblProducto?> GetByNombreAsync(string nombre);
        Task<IEnumerable<TblProducto>> GetActiveAsync();
        Task<TblProducto?> GetByCategoriaAsync(int idCategoria);
        Task<TblProducto?> GetByMarcaAsync(int idMarca);
        Task<IEnumerable<TblProducto>> ObtenerProductosStockBajoAsync();
        Task<TblProducto?> SearchByCodigoAsync(string codigo);
        Task<TblProducto?> SearchByNombreAsync(string nombre);
    }
}