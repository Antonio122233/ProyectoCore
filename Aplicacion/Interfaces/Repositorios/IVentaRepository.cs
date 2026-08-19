using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Repositorios
{
    public interface IVentaRepository : IGenericRepository<TblVenta>
    {
        Task<TblVenta?> GetVentaCompletaAsync(int id);
        Task<IEnumerable<TblVenta>> GetVentasCompletasAsync();
    }
}
