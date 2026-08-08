using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Repositorios
{
    public interface ICompraRepository : IGenericRepository<TblCompra>
    {
        //sino hay resultados se devuelve una lista vacia
        Task<IEnumerable<TblCompra>> GetByFechaAsync(DateTime Fecha);
        Task<IEnumerable<TblCompra>> GetByProveedor(int idProveedor);
        Task<TblCompra?> GetCompraCompletaAsync(int id);
        Task<IEnumerable<TblCompra>> GetComprasCompletasAsync();
    }
}
